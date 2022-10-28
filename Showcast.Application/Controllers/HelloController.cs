using Microsoft.AspNetCore.Mvc;

namespace Showcast.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HelloController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Ok");
    }
}