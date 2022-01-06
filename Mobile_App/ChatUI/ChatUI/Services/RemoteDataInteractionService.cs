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

        private SessionToken _sessionToken;
        private readonly APIWorkerService _workerService;
        private readonly IIOManager _iOManager;

        #endregion Fields

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
            var users = await _workerService.GetUsersAround(_sessionToken, new Geoposition(longitude, latitude));
            return users.UsersArray;
        }

        public async Task<bool> LoginAsync(string userName, string password)
        {
            var pairsToken = default(PairTokenResponse);
            try
            {
                if (!InitSessionToken())
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
            catch
            {
                return false;
            }

            if (pairsToken != null)
            {
                _sessionToken.UpdateByPairTokenResponse(pairsToken);
            }

            return IsValidData;
        }

        public async Task<bool> Registration(string userName, string password)
        {
            var sessionToken = new SessionToken(userName, password);
            var userTokenResponse = default(UserTokenResponse);
            try
            {
                userTokenResponse = await _workerService.CreateAccountAsync(sessionToken);
            }
            catch
            {
                return false;
            }

            if (userTokenResponse == null)
                return false;

            sessionToken.UpdateByUserTokenResponse(userTokenResponse, password);

            return IsValidData;
        }

        public Task<Common.Models.DTOs.User> UpdateUserDataAsync(Common.Models.DTOs.User user)
        {
            throw new System.NotImplementedException();
        }

        private bool InitSessionToken()
        {
            if (_sessionToken == null)
                _sessionToken = new SessionToken(GetAccessToken(PathHelper.UserCredentialFile));

            return _sessionToken != null;
        }

        private AccessToken GetAccessToken(string path) =>
            _iOManager.ReadAll<AccessToken>(path);

    }
}
