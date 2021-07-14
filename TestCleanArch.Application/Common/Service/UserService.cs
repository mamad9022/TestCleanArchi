using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TestCleanArch.Application.Authorize;
using TestCleanArch.Application.Common.Dtos;
using TestCleanArch.Application.Common.Exceptions;
using TestCleanArch.Application.Common.Interface;
using TestCleanArch.Domain.Models;

namespace TestCleanArch.Application.Common.Service
{
    public class UserService : IUserService
    {
        private readonly ITestCleanArchDbContext _context;
        private readonly IDistributedCache _distributedCache;
        private readonly AppSettings _appSettings;
        private readonly IMemoryCache _cache;

        public UserService(ITestCleanArchDbContext context, IOptions<AppSettings> appSettings, IDistributedCache distributedCache, IMemoryCache cache)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _distributedCache = distributedCache;
            _cache = cache;
        }
        public async Task<AuthenticateResponse> Authenticate(UserRequset request, HttpContext httpContext)
        {
            string ip = httpContext.Items["UserIp"]?.ToString();

            var ipExist = await _distributedCache.GetStringAsync(ip, default);
            if (ipExist != null)
            {
                throw new Exception("حساب کاربری شما به مذت 2 دقیقه مسدود شده است");
            }
            var user = await _context.Persons.SingleOrDefaultAsync(x => x.Username == request.Username && x.Password == request.Password);

            // return null if user not found
            if (user == null)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                int counter = _cache.Get<int>(ip);
                if (counter == 3)
                {
                    await _distributedCache.SetRecordAsync(ip, ip, default);
                }
                counter++;
                _cache.Set(ip, counter, cacheEntryOptions);

                return null;
            }

            // authentication successful so generate jwt token
            var token = GenerateJwtToken(user);

            return new AuthenticateResponse(token);
        }

        public async Task<Person> GetByIdAsync(Guid userId)
        {
            var user = await _context.Persons.AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == userId);

            if (user is null)
                throw new NotFoundException();

            return user;

        }

        private string GenerateJwtToken(Person user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
