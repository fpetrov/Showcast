using Microsoft.Extensions.Options;
using Showcast.Core.Entities.Authentication;
using Showcast.Core.Services.Security;

namespace Showcast.Infrastructure.Services.Security;

public class TokenService : ITokenService
{
    private readonly AuthenticationOptions _options;
    
    public TokenService(IOptions<AuthenticationOptions> options)
    {
        _options = options.Value;
    }
    
    public (string token, RefreshToken refreshToken) GenerateTokenPair(User user, string fingerprint)
    {
        return (GenerateToken(user), GenerateRefreshToken(user, fingerprint));
    }

    private string GenerateToken(User user)
    {
        return "";
    }

    private RefreshToken GenerateRefreshToken(User user, string fingerprint)
    {
        return new RefreshToken(1, "", fingerprint, DateTime.Now.AddDays(30));
    }
}