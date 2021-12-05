using Common.Abstractions;
using Common.Models.Requests;

namespace Common.Models.DTOs
{
    public class User : BaseObject
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Rate { get; set; }

        public User(AuthenticationUserTokenRequset request, string hashedPassword)
        {
            UserName = request.Client.UserName;
            Email = request.Client.Email;
            Password = hashedPassword;
            Rate = 3;
        }

    }
}
