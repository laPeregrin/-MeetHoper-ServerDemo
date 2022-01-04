using System;

namespace ServerHandler.Models
{
    public class AccessToken
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AccessJWTToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
