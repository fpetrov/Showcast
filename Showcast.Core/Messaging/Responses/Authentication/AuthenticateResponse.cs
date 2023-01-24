using Showcast.Core.Entities.Authentication;

namespace Showcast.Core.Messaging.Responses.Authentication;

public record AuthenticateResponse(
    int Id,
    string Jwt,
    RefreshToken RefreshToken
);