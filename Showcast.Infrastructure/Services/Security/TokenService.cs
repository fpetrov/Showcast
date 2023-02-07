using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
        return (GenerateToken(user), GenerateRefreshToken(fingerprint));
    }

    private string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecurityKey));
        
        var jwt = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            notBefore: DateTime.UtcNow,
            claims: GetClaims(user).Claims,
            expires: DateTime.UtcNow.Add(_options.TokenLifetime),
            signingCredentials: new SigningCredentials(
                symmetricSecurityKey,
                SecurityAlgorithms.HmacSha256));

        return tokenHandler.WriteToken(jwt);
    }
    
    private static ClaimsIdentity GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new("name", user.Name),
            new("role", user.Role.ToString())
        };

        var claimsIdentity = new ClaimsIdentity(claims, "Token", "name", "role");

        return claimsIdentity;
    }

    private RefreshToken GenerateRefreshToken(string fingerprint)
    {
        var key = RandomNumberGenerator.GetBytes(128);

        return new RefreshToken
        {
            Body = Convert.ToBase64String(key),
            Fingerprint = fingerprint,
            Expires = DateTime.UtcNow.Add(_options.RefreshTokenLifetime)
        };
    }
}