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

        public async Task<PairTokenResponse> AuthenticateAsync(AuthenticationUserTokenRequset userRequest)
        {
            var user = await _userHandler.GetEntityByIdAsync(userRequest.Client.Id);
            if (user == null                                 ||
                user.UserName != userRequest.Client.UserName || 
                !await _passwordHasher.CompareLinesAsync(user.Password, userRequest.Client.Password))
                throw new Exception();

            return GeneratePairToken();
        }

        public async Task<UserResponse> CreateAccount(AuthenticationUserTokenRequset userLoginRequest)
        {
            var user = await _userHandler.GetEntityByActionAsync(u => u.UserName == userLoginRequest.Client.UserName ||
                                                                      u.Email == userLoginRequest.Client.Email);

            if (user != null)
                throw new Exception(Constants.AlreadyExist);

            var hashedPassword = await _passwordHasher.CryptLineAsync(userLoginRequest.Client.Password);
            var newUser = new User(userLoginRequest, hashedPassword);
            await _userHandler.SaveEntityAsync(newUser);
            var pairToken = GeneratePairToken();
            return new UserResponse(newUser, pairToken);
        }

        private PairTokenResponse GeneratePairToken() =>
            new PairTokenResponse(JWTGenerator.GenerateToken(_settings),
                                  JWTGenerator.GenerateToken(_settings, true));

    }
}
