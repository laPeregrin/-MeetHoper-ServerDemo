namespace Common.Models.Responses
{
    public class UserResponse : UserPublicDataResponse
    {
        public string Email { get; set; }

        public UserResponse()
        {

        }

        public UserResponse(DTOs.User user)
        {
            Id = user.Id;
            UserName = user.UserName;
            Email = user.Email;
            Description = user.Description;
            ImageUrl = user.ImageUrl;
            Rate = user.Rate;
        }
    }
}
