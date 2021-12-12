using Common.Models.Requests;
using Common.Models.Responses;
using ServerHandler.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerHandler.Services
{
    internal class APIWorkerService
    {
        private const string AuthorizationHeader = "Authorization";

        private const string GetPairsToken = "/Authenticate/GetPairTokens";
        private const string CreateAccount = "/Authenticate/CreateAccount";
        private const string UpdateAccount = "/Authenticate/UpdateAccount";

        private readonly HttpWorker _httpWorker;
        private readonly string _baseAddress;

        public APIWorkerService(string baseAddress, HttpWorker httpWorker)
        {
            _httpWorker = httpWorker;
            _baseAddress = baseAddress;
        }

        public async Task<PairTokenResponse> GetPairTokens(AccessToken accessToken)
        {
            var headers = new Dictionary<string, string>();
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
            return await _httpWorker.PostByRequestAsync<AuthenticationUserTokenRequset, PairTokenResponse>(link, request, headers);
        }

        public async Task<UserTokenResponse> CreateAccountAsync(AccessToken accessToken)
        {
            var headers = new Dictionary<string, string>();
            var request = new CreateAccountRequest()
            {
                Email = accessToken.Email,
                UserName = accessToken.UserName,
                Password = accessToken.Password,
            };

            var link = _baseAddress + CreateAccount;
            return await _httpWorker.PostByRequestAsync<CreateAccountRequest, UserTokenResponse>(link, request, headers);
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

            headers.Add(AuthorizationHeader, $"Bearer {accessToken.AccessJWTToken}");
            var link = _baseAddress + UpdateAccount;
            return await _httpWorker.PostByRequestAsync<UpdateUserDataRequest, bool>(link, request, headers);
        }

    }
}
