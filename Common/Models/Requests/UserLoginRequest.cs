using Common.Abstractions;
using System;

namespace Common.Models.Requests
{
    public class UserLoginRequest : BaseObject
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Passwrd { get; set; }
    }
}
