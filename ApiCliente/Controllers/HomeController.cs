using Microsoft.AspNetCore.Mvc;

namespace ApiCliente.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("v1/health-check")]
        public IActionResult Get()
        {
            return Ok(new{ message = "API_OK"});
        }
    }
}
