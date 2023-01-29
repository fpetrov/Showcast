using Microsoft.AspNetCore.Mvc;
using Showcast.Infrastructure;

namespace Showcast.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HelloController : ControllerBase
{

    public HelloController()
    {
        
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("");
    }
}