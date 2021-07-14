using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using TestCleanArch.Application.Authorize;
using TestCleanArch.Application.Common.Interface;

namespace TestCleanArch.Api.Controllers
{
    public class AuthenticationController : Controller
    {
        protected readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }
        
        [AllowAnonymous]
        [EnableCors]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserRequset command)
        {
            var result = await _userService.Authenticate(command, HttpContext);
            if (result is null)
            return Unauthorized();
          
            return Ok(result);

        }
    }
}
