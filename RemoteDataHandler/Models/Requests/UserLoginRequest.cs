﻿namespace Common.Models.Requests
{
    public class UserLoginRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
