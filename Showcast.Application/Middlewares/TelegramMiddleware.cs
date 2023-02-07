using MediatR;
using Showcast.Application.Extensions;
using Showcast.Infrastructure.Messaging.Authentication.Commands;

namespace Showcast.Application.Middlewares;

public class TelegramMiddleware
{
    private readonly RequestDelegate _next;

    public TelegramMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IMediator mediator)
    {
        if (context.Request.Headers.ContainsKey("Telegram-Id"))
        {
            var telegramId = long.Parse(context.Request.Headers["Telegram-Id"]!);

            var response = await mediator.Send(new RefreshTokenCommand(string.Empty, "Telegram" + telegramId, telegramId));

            if (response == null)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
            }
            else
            {
                context.AppendAuthorizationHeader(response.Jwt);
            }
        }
        
        await _next.Invoke(context);
    }
}