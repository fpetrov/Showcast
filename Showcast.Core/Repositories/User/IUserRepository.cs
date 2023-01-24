using System.Linq.Expressions;
using Showcast.Core.Messaging.Requests.Authentication;
using Showcast.Core.Messaging.Responses.Authentication;

namespace Showcast.Core.Repositories.User;

public interface IUserRepository //: IRepository<Entities.Authentication.User>
{
    public Task<AuthenticateResponse?> Authenticate(
        AuthenticateRequest request,
        Func<Entities.Authentication.User, bool> passwordVerifier,
        CancellationToken cancellationToken = default);

    public Task<AuthenticateResponse?> Create(
        CreateUserRequest request,
        Expression<Func<Entities.Authentication.User, bool>>? duplicatePredicate = default,
        CancellationToken cancellationToken = default);

    public Task<AuthenticateResponse?> RefreshToken(
        RefreshTokenRequest request,
        CancellationToken cancellationToken = default);

    public Task<bool> RevokeToken(
        RevokeTokenRequest request,
        CancellationToken cancellationToken = default);
}