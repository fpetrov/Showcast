namespace Showcast.Core.Messaging.Requests.Authentication;

public record RefreshTokenRequest(
    string? Token,
    string Fingerprint
);