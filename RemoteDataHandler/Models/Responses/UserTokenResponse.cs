namespace Common.Models.Responses
{
    public class UserTokenResponse : TokenResponseContainer
    {
        public UserResponse UserData { get; set; }

        public UserTokenResponse(DTOs.User user, PairTokenResponse pairTokenResponse)
        {
            UserData = new UserResponse(user);
            TokenPair = pairTokenResponse;
        }
    }
}
