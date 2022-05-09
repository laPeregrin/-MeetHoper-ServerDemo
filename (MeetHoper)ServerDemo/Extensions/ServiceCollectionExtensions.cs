using _MeetHoper_ServerDemo.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace _MeetHoper_ServerDemo.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddJwtSwaggerAuthentication(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var jwtTokenSettings = configuration.GetSection("Authentication").Get<AuthenticationModel>();
            configuration.BindJwtSetting(jwtTokenSettings);
            serviceCollection.AddSingletonJwtSetting(jwtTokenSettings);
            serviceCollection.Configure<AuthenticationModel>(configuration.GetSection("Authentication"));
            serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(a =>
            {
                a.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenSettings.AccessToken)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtTokenSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtTokenSettings.Audience,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        private static void BindJwtSetting(this IConfiguration configuration, AuthenticationModel authenticationModel) =>
            configuration.Bind(authenticationModel);

        private static void AddSingletonJwtSetting(this IServiceCollection serviceCollection, AuthenticationModel authenticationModel) =>
            serviceCollection.AddSingleton(authenticationModel);
    }
}
