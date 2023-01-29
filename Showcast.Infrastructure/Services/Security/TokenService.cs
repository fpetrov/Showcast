using System.Text;
using LitJWT;
using LitJWT.Algorithms;
using Microsoft.Extensions.Options;
using Showcast.Core.Entities.Authentication;
using Showcast.Core.Services.Security;

namespace Showcast.Infrastructure.Services.Security;

public class TokenService : ITokenService
{
    private readonly JwtEncoder _encoder;
    private readonly AuthenticationOptions _options;

    public TokenService(IOptions<AuthenticationOptions> options)
    {
        _options = options.Value;
        _encoder = new JwtEncoder(new HS256Algorithm(Encoding.UTF8.GetBytes(_options.SecurityKey)));
    }
    
    public (string token, RefreshToken refreshToken) GenerateTokenPair(User user, string fingerprint)
    {
        return (GenerateToken(user), GenerateRefreshToken(user, fingerprint));
    }

    private string GenerateToken(User user)
    {
        var payload = new
        {
            Name = user.Name,
            Role = user.Role.ToString()
        };

        var token = _encoder.Encode(payload, _options.TokenLifetime);
        
        return token;
    }

    private RefreshToken GenerateRefreshToken(User user, string fingerprint)
    {
        var key = HS256Algorithm.GenerateRandomRecommendedKey();

        return new RefreshToken(
            1,
            Convert.ToBase64String(key),
            fingerprint,
            DateTime.UtcNow.Add(_options.RefreshTokenLifetime));
    }
}