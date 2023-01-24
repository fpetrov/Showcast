namespace Showcast.Core.Entities.Authentication;

public record AuthenticationOptions(
    string SecurityKey,
    string Issuer,
    string Audience,
    TimeSpan TokenLifetime,
    TimeSpan RefreshTokenLifetime
)
{
    public static AuthenticationOptions Default = new AuthenticationOptions(
        "Showcast.Key",
        "Showcast.Application",
        "Showcast.UI",
        TimeSpan.FromMinutes(60),
        TimeSpan.FromDays(30)
    );
}