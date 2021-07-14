using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using TestCleanArch.Application.Authorize;
using TestCleanArch.Application.Common.Dtos;
using TestCleanArch.Domain.Models;

namespace TestCleanArch.Application.Common.Interface
{
    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(UserRequset request, HttpContext httpContext);
        Task<Person> GetByIdAsync(Guid userId);
    }
}
