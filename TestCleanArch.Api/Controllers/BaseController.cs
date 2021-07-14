using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TestCleanArch.Api.Authorize;

namespace TestCleanArch.Api.Controllers
{
    [Route("[Controller]")]
    [EnableCors]
    [ApiController]
    [Authorize]
    public class BaseController : Controller
    {
    }
}
