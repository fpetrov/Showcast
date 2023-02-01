using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Showcast.Application.Extensions;
using Showcast.Infrastructure.Messaging.Authentication.Commands;

namespace Showcast.Application.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UserController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var response = await _mediator.Send(command);
        
        if (response == null)
            return BadRequest(new { message = "User with this name already exists!" });

        HttpContext.AppendRefreshToken(response.RefreshToken);

        return Ok(response);
    }
    
    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateCommand command)
    {
        var response = await _mediator.Send(command);
        
        if (response == null)
            return BadRequest(new { message = "User's fields are incorrect!" });

        HttpContext.AppendRefreshToken(response.RefreshToken);

        return Ok(response);
    }
    
    [AllowAnonymous]
    [HttpPost("refreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        var token = command.Token ?? HttpContext.GetRefreshToken();

        command = command with
        {
            Token = token
        };

        if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(command.Fingerprint))
            return BadRequest(new { message = "Invalid token or fingerprint!" });

        var response = await _mediator.Send(command);

        if (response == null)
        {
            HttpContext.RemoveRefreshToken();
            
            return NotFound(new { message = "Token or fingerprint were not found!" });
        }
        
        HttpContext.AppendRefreshToken(response.RefreshToken);

        return Ok(response);
    }
    
    [AllowAnonymous]
    [HttpPost("revokeToken")]
    public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenCommand command)
    {
        var token = command.Token ?? HttpContext.GetRefreshToken();

        command = command with
        {
            Token = token
        };
        
        if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(command.Fingerprint))
            return BadRequest(new { message = "Token or fingerprint were required!" });

        var response = await _mediator.Send(command);

        if (!response)
            return NotFound(new { message = "Token or fingerprint was not found!" });
        
        HttpContext.RemoveRefreshToken();

        return Ok(new { message = "Token revoked successfully!" });
    }
}