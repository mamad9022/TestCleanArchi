using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TestCleanArch.Api.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class UserIpMiddleware
    {
        private readonly RequestDelegate _next;

        public UserIpMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var clientIp = httpContext.Connection.RemoteIpAddress?.ToString();
            httpContext.Items["UserIp"] = clientIp;
            await _next(httpContext);
        }
    }
}
