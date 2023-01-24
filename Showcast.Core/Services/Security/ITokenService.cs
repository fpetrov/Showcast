using Showcast.Core.Entities.Authentication;

namespace Showcast.Core.Services.Security;

public interface ITokenService
{
    public (string token, RefreshToken refreshToken) GenerateTokenPair(User user, string fingerprint);
}