using Common.Abstractions;
using Common.Models.Requests;
using System;

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

        public User() { }

        public User(CreateAccountRequest request, string hashedPassword)
        {
            Id = Guid.NewGuid();
            UserName = request.UserName;
            Email = request.Email;
            Password = hashedPassword;
            Rate = 3;
        }

        public void UpdateByRequest(UpdateUserDataRequest request)
        {
            //UserName = request.UserName;
            //Email = request.Email;
            Password = request.Password;
            Description = request.Description;
            ImageUrl = request.ImageUrl;
        }

    }
}
