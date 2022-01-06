using Common.Models.Responses;
using ServerHandler.Models;
using System;

namespace ChatUI.Models
{
    public class SessionToken : AccessToken
    {
        public DateTime DateExpiration { get; set; }

        public string Description { get; set; }

        public SessionToken(string userName, string password, string description = "")
        {
            UserName = userName;
            Password = password;
            Description = description;
        }

        public SessionToken(AccessToken accessToken)
        {
            UserId = accessToken.UserId;
            UserName = accessToken.UserName;
            Password = accessToken.Password;
            Email = accessToken.Email;
            RefreshToken = accessToken.RefreshToken;
            AccessJWTToken = accessToken.AccessJWTToken;
        }

        public void UpdateByUserTokenResponse(UserTokenResponse userToken, string password)
        {
            UserId = userToken.UserData.Id;
            UserName = userToken.UserData.UserName;
            Email = userToken.UserData.Email;
            UpdateByPairTokenResponse(userToken.TokenPair);
        }

        public void UpdateByPairTokenResponse(PairTokenResponse pairToken)
        {
            if (pairToken == null)
                return;

            AccessJWTToken = pairToken.AccessToken;
            RefreshToken = pairToken.RefreshToken;
            DateExpiration = DateTime.UtcNow.AddMinutes(pairToken.Expiration);
        }
    }
}
