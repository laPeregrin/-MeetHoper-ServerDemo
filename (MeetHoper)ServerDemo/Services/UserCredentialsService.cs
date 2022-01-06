using _MeetHoper_ServerDemo.Helpers;
using _MeetHoper_ServerDemo.Models;
using Common;
using Common.Abstractions;
using Common.Models.DTOs;
using Common.Models.Requests;
using Common.Models.Responses;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace _MeetHoper_ServerDemo.Services
{
    public class UserCredentialsService
    {
        #region Fields

        private readonly IDataBaseUserHandler _userHandler;
        private readonly PasswordHasher _passwordHasher;
        private readonly AuthenticationModel _settings;

        #endregion //Fields

        public UserCredentialsService(IDataBaseUserHandler userHandler,
                                      PasswordHasher passwordHasher,
                                      IOptions<AuthenticationModel> settings)
        {
            _userHandler = userHandler;
            _passwordHasher = passwordHasher;
            _settings = settings.Value;
        }

        public async Task<PairTokenResponse> LoginAsync(UserLoginRequest userRequest)
        {
            await ValidateUserData(userRequest);

            return GeneratePairToken(_settings.AccessTokenExpirationMinutes);
        }

        public async Task<PairTokenResponse> GetPairTokensAsync(AuthenticationUserTokenRequset userRequest)
        {
            await ValidateUserData(userRequest.Client);

            if (JWTGenerator.Validate(_settings, userRequest.RefreshToken))
                throw new Exception(Constants.RefreshTokentNotValid);

            return GeneratePairToken(_settings.AccessTokenExpirationMinutes);
        }

        public async Task<UserTokenResponse> CreateAccount(CreateAccountRequest userLoginRequest)
        {
            var user = await _userHandler.GetEntityByActionAsync(u => u.UserName == userLoginRequest.UserName ||
                                                                      u.Email == userLoginRequest.Email);

            if (user != null)
                throw new Exception(Constants.AlreadyExist);

            var hashedPassword = await _passwordHasher.CryptLineAsync(userLoginRequest.Password);
            var newUser = new User(userLoginRequest, hashedPassword);
            await _userHandler.SaveEntityAsync(newUser);
            var pairToken = GeneratePairToken(_settings.AccessTokenExpirationMinutes);
            return new UserTokenResponse(newUser, pairToken);
        }

        public async Task<bool> UpdateAccount(UpdateUserDataRequest userLoginRequest)
        {
            var user = await _userHandler.GetEntityByActionAsync(u => u.UserName == userLoginRequest.UserName ||
                                                                      u.Email == userLoginRequest.Email);

            if (user == null)
                throw new Exception(Constants.DoesNotExistText);

            userLoginRequest.Password = await _passwordHasher.CryptLineAsync(userLoginRequest.Password);
            user.UpdateByRequest(userLoginRequest);
            return await _userHandler.UpdateEntityAsync(user);
        }

        private async Task<bool> ValidateUserData(UserLoginRequest userLoginRequest)
        {
            var user = await _userHandler.GetEntityByIdAsync(userLoginRequest.Id);

            if (user == null || user.UserName != userLoginRequest.UserName ||
                !await _passwordHasher.CompareLinesAsync(userLoginRequest.Password, user.Password))
                throw new Exception(Constants.DoesNotExistText);

            return true;
        }

        private PairTokenResponse GeneratePairToken(int expiration) =>
            new PairTokenResponse(JWTGenerator.GenerateToken(_settings),
                                  JWTGenerator.GenerateToken(_settings, true),
                                  expiration);

    }
}
