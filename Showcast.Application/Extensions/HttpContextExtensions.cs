using Showcast.Core.Entities.Authentication;

namespace Showcast.Application.Extensions;

public static class HttpContextExtensions
{
    private const string RefreshTokenCookieName = "refreshToken";

    public static IResponseCookies AppendRefreshToken(this HttpContext context, RefreshToken refreshToken)
    {
        context.Response.Cookies.Append(RefreshTokenCookieName, refreshToken.Body, new CookieOptions
        {
            Secure = true,
            HttpOnly = true,
            Expires = refreshToken.Expires
        });

        return context.Response.Cookies;
    }

    public static bool TryGetRefreshToken(this HttpContext context, out string refreshToken)
    {
        refreshToken = context.Request.Cookies[RefreshTokenCookieName]!;

        return !string.IsNullOrEmpty(refreshToken);
    }
    
    public static string? GetRefreshToken(this HttpContext context)
    {
        return context.Request.Cookies[RefreshTokenCookieName];
    }

    public static void RemoveRefreshToken(this HttpContext context)
    {
        context.Response.Cookies.Append(RefreshTokenCookieName, string.Empty, new CookieOptions
        {
            Expires = DateTime.Now.AddDays(-1)
        });
    }
}