namespace Common.Models.Responses
{
    public class PairTokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int Expiration { get; set; }

        public PairTokenResponse() { }

        public PairTokenResponse(string accessToken, string refreshToken, int expiration)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            Expiration = expiration;
        }
    }
}
