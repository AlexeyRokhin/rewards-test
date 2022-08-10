using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Rewards.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
