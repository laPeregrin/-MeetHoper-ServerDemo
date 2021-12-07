namespace Common.Models.Responses
{
    public class PairTokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public PairTokenResponse() { }

        public PairTokenResponse(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
