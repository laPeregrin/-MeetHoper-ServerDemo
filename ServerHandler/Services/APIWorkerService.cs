using Common.Models.DTOs;
using Common.Models.Requests;
using Common.Models.Responses;
using ServerHandler.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerHandler.Services
{
    internal class APIWorkerService
    {
        private const string AuthorizationHeader = "Authorization";
        private const string AuthenticateEndpoint = "Authenticate";
        private const string GeoDataNavigationEndpoint = "GeoDataNavigation/GetUsersNear";

        private const string GetPairsToken = AuthenticateEndpoint + "/GetPairTokens";
        private const string CreateAccount = AuthenticateEndpoint + "/CreateAccount";
        private const string UpdateAccount = AuthenticateEndpoint + "/UpdateAccount";

        private readonly HttpWorker _httpWorker;
        private readonly string _baseAddress;

        public APIWorkerService(string baseAddress, HttpWorker httpWorker)
        {
            _httpWorker = httpWorker;
            _baseAddress = baseAddress;
        }

        public async Task<PairTokenResponse> GetPairTokens(AccessToken accessToken)
        {
            var request = new AuthenticationUserTokenRequset()
            {
                RefreshToken = accessToken.RefreshToken,
                Client = new UserLoginRequest()
                {
                    Id = accessToken.UserId,
                    Email = accessToken.Email,
                    UserName = accessToken.UserName,
                    Password = accessToken.Password,
                }
            };
            
            var link = _baseAddress + GetPairsToken;
            return await _httpWorker.PostByRequestAsync<AuthenticationUserTokenRequset, PairTokenResponse>(link, request);
        }

        public async Task<UserTokenResponse> CreateAccountAsync(AccessToken accessToken)
        {
            var request = new CreateAccountRequest()
            {
                Email = accessToken.Email,
                UserName = accessToken.UserName,
                Password = accessToken.Password,
            };

            var link = _baseAddress + CreateAccount;
            return await _httpWorker.PostByRequestAsync<CreateAccountRequest, UserTokenResponse>(link, request);
        }

        public async Task<bool> UpdateAccountAsync(AccessToken accessToken, string description, string img)
        {
            var headers = new Dictionary<string, string>();
            var request = new UpdateUserDataRequest()
            {
                Id = accessToken.UserId,
                Email = accessToken.Email,
                UserName = accessToken.UserName,
                Description = description,
                ImageUrl = img,
                Password = accessToken.Password,
            };

            CompleteHeaderByAccessToken(headers, accessToken);
            var link = _baseAddress + UpdateAccount;
            return await _httpWorker.PostByRequestAsync<UpdateUserDataRequest, bool>(link, request, headers);
        }

        public async Task<UserCollectionResponse> GetUsersAround(AccessToken accessToken, Geoposition geopostion)
        {
            var headers = new Dictionary<string, string>();
            CompleteHeaderByAccessToken(headers, accessToken);
            var query = string.Format("/?UserId={0}&longitude={1}&latitude={2}", accessToken.UserId,
                                                                                 geopostion.Longitude,
                                                                                 geopostion.Latitude);

            var link = _baseAddress + GeoDataNavigationEndpoint + query;
            return await _httpWorker.GetLinkAsync<UserCollectionResponse>(link, headers);
        }

        private void CompleteHeaderByAccessToken(Dictionary<string, string> headers, AccessToken accessToken)
        {
            if (headers == null)
                throw new ArgumentNullException();

            headers[AuthorizationHeader] = $"Bearer {accessToken.AccessJWTToken}";
        }
    }
}

