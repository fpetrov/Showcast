using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Showcast.Infrastructure.Services.Http;

namespace Showcast.Application.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MovieController : ControllerBase
{
    private readonly RecommendationService _recommendationService;
    
    public MovieController(RecommendationService recommendationService)
    {
        _recommendationService = recommendationService;
    }
    
    [HttpGet("relative")]
    public async Task<IActionResult> GetRelative([FromQuery] string movieName)
    {
        return Ok(await _recommendationService.GetRelativeMovies(movieName));
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Ok!!!!!!");
    }
}