using MediatR;
using Showcast.Infrastructure.Messaging.Authentication.Commands;

namespace Showcast.Application.Middlewares;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IMediator mediator)
    {
        var name = context.User.FindFirst("name")?.Value;

        if (!string.IsNullOrEmpty(name))
        {
            var user = await mediator.Send(new FindUserCommand(name));
            
            if (user != null)
                context.Request.HttpContext.Items.Add("User", user);
        }
        
        await _next.Invoke(context);
    }
}