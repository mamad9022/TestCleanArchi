using System;
using TestCleanArch.Domain.Models;

namespace TestCleanArch.Application.Common.Dtos
{
    public class AuthenticateResponse
    {
        public string Token { get; set; }

        public AuthenticateResponse(string token)
        {
            Token = token;
        }
    }
}
