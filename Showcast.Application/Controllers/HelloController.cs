using Microsoft.AspNetCore.Mvc;
using Showcast.Infrastructure;

namespace Showcast.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HelloController : ControllerBase
{
    // private readonly Greeter.GreeterClient _greeterClient;
    //
    // public HelloController(Greeter.GreeterClient greeterClient)
    // {
    //     _greeterClient = greeterClient;
    // }
    //
    // [HttpGet]
    // public IActionResult Get()
    // {
    //     return Ok(_greeterClient.SayHello(new HelloRequest { Name = "Fedor!" }));
    // }
}