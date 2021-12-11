﻿using _MeetHoper_ServerDemo.Helpers;
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
                !await _passwordHasher.CompareLinesAsync(userRequest.Client.Password, user.Password))
                throw new Exception(Constants.DoesNotExistText);

            return GeneratePairToken();
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
            var pairToken = GeneratePairToken();
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

        private PairTokenResponse GeneratePairToken() =>
            new PairTokenResponse(JWTGenerator.GenerateToken(_settings),
                                  JWTGenerator.GenerateToken(_settings, true));

    }
}
