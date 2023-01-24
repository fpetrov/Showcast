namespace Showcast.Core.Messaging.Requests.Authentication;

public record RevokeTokenRequest(
    string Token, 
    string Fingerprint
);