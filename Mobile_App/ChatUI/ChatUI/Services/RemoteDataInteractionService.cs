using ChatUI.Abstractions;
using ChatUI.Helpers;
using ChatUI.Models;
using Common.Models.DTOs;
using Common.Models.Responses;
using ServerHandler.Models;
using ServerHandler.Services;
using System;
using System.Threading.Tasks;

namespace ChatUI.Services
{
    public class RemoteDataInteractionService : IAPIInteraction
    {
        #region Fields

        private readonly APIWorkerService _workerService;
        private readonly IIOManager _iOManager;
        private SessionToken _sessionToken;

        #endregion Fields

        public SessionToken Token => _sessionToken;

        public bool IsTokenExpired => _sessionToken?.DateExpiration < DateTime.UtcNow;
        public bool IsValidData => _sessionToken != null &&
                                   !string.IsNullOrEmpty(_sessionToken.AccessJWTToken) &&
                                   !string.IsNullOrEmpty(_sessionToken.RefreshToken) &&
                                   !IsTokenExpired;

        public RemoteDataInteractionService(RemoteSettings remoteSettings, IIOManager iOManager)
        {
            _workerService = new APIWorkerService(remoteSettings.BaseAdress);
            _iOManager = iOManager;
        }

        public async Task<UserPublicDataResponse[]> GetUsersByGeolocationAsync(double longitude, double latitude)
        {
            var result = default(UserCollectionResponse);
            try
            {
                result = await _workerService.GetUsersAround(_sessionToken, new Geoposition(longitude, latitude));
            }
            catch
            {
                return Array.Empty<UserPublicDataResponse>();
            }

            return result.UsersArray;
        }

        public async Task<bool> LoginAsync(string userName, string password)
        {
            var pairsToken = default(PairTokenResponse);
            try
            {
                if (!InitSessionToken(userName, password))
                {
                    pairsToken = await _workerService.LoginAsync(new AccessToken()
                    {
                        UserName = userName,
                        Password = password
                    });
                }
                else
                {
                    pairsToken = await _workerService.GetPairTokensAsync(_sessionToken);
                }
            }
            catch (Exception e)
            {
                return false;
            }

            if (pairsToken != null)
                _sessionToken.UpdateByPairTokenResponse(pairsToken);

            SaveAccessTokenValid(_sessionToken);

            return IsValidData;
        }

        public async Task<bool> Registration(string userName, string password, string email)
        {
            InitSessionToken(userName, password, email);
            var userTokenResponse = default(UserTokenResponse);
            try
            {
                userTokenResponse = await _workerService.CreateAccountAsync(_sessionToken);
            }
            catch (Exception e)
            {
                return false;
            }

            if (userTokenResponse == null)
                return false;

            _sessionToken.UpdateByUserTokenResponse(userTokenResponse, password);
            SaveAccessTokenValid(_sessionToken);

            return IsValidData;
        }

        public async Task<bool> UpdateUserDataAsync(Common.Models.DTOs.User user)
        {
            var res = false;
            try
            {
                res = await _workerService.UpdateAccountAsync(_sessionToken, user.Description, user.ImageUrl);
                if (res)
                {
                    _sessionToken.Description = user.Description;
                    _sessionToken.UserName = user.UserName;
                    _sessionToken.Password = user.Password;
                    SaveAccessTokenValid(_sessionToken);
                }
            }
            catch
            {
                return false;
            }


            return res;
        }

        private bool InitSessionToken(string userName, string password, string email = null)
        {
            if (!IsValidData)
            {
                _sessionToken = GetAccessToken(PathHelper.UserCredentialFile);
                _sessionToken.UserName = userName;
                _sessionToken.Password = password;
                _sessionToken.Email = email;
            }

            return IsValidData;
        }

        private SessionToken GetAccessToken(string path) =>
            _iOManager.ReadAll<SessionToken>(path) ?? new SessionToken();

        private void SaveAccessTokenValid(SessionToken sessionToken)
        {
            if (IsValidData)
            {
                _iOManager.Write(PathHelper.UserCredentialFile, sessionToken);
            }
        }

    }
}
