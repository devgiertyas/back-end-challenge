using Microsoft.AspNetCore.Mvc;

namespace TodoAPI.Controllers
{
    [ApiController]
    [Route("api/todo")]
    public class UserController : ControllerBase
    {

          
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Teste");
        }

    }
}
