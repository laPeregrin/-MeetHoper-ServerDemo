namespace Common.Models.Responses
{
    public class UserResponse
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Rate { get; set; }

        public UserResponse(DTOs.User user)
        {
            UserName = user.UserName;
            Email = user.Email;
            Description = user.Description;
            ImageUrl = user.ImageUrl;
            Rate = user.Rate;
        }
    }
}
