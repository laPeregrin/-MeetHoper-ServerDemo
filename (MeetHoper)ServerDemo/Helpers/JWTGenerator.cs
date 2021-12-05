using _MeetHoper_ServerDemo.Models;
using Common;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace _MeetHoper_ServerDemo.Helpers
{
    public static class JWTGenerator
    {
        public static string GenerateToken(AuthenticationModel model, bool isRefreshToken = false)
        {
            var accessToken = isRefreshToken ? Constants.RefreshTokenText + model.AccessToken :
                                               model.AccessToken;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(accessToken));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                model.Issuer,
                model.Audience,
                null,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(model.AccessTokenExpirationMinutes),
                credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static bool Validate(AuthenticationModel model, string refreshToken)
        {
            var validationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                                                            Constants.RefreshTokenText + model.AccessToken)),
                ValidIssuer = model.Issuer,
                ValidAudience = model.Audience,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(refreshToken, validationParameters, out SecurityToken Validatetoken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
