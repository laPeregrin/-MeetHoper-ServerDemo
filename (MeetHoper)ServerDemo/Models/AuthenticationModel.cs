namespace _MeetHoper_ServerDemo.Models
{
    public class AuthenticationModel
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string AccessToken { get; set; }
        public int AccessTokenExpirationMinutes { get; set; }
    }
}
