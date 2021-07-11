using System;
using TestCleanArch.Domain.Models;

namespace TestCleanArch.Application.Common.Dtos
{
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(Person user, string token)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            Password = user.Password;
            Token = token;
        }
    }
}
