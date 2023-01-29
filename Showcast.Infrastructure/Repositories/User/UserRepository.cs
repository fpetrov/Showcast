using System.Linq.Expressions;
using AutoMapper;
using Showcast.Core.Messaging.Requests.Authentication;
using Showcast.Core.Messaging.Responses.Authentication;
using Showcast.Core.Repositories.User;
using Showcast.Core.Services.Security;
using Showcast.Infrastructure.Contexts;

namespace Showcast.Infrastructure.Repositories.User;

public class UserRepository : RepositoryBase<Core.Entities.Authentication.User, ApplicationContext>, IUserRepository
{
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    
    public UserRepository(ApplicationContext databaseContext, ITokenService tokenService, IMapper mapper)
        : base(databaseContext)
    {
        _tokenService = tokenService;
        _mapper = mapper;
    }

    public async Task<AuthenticateResponse?> Authenticate(
        AuthenticateRequest request,
        Func<Core.Entities.Authentication.User, bool> passwordVerifier,
        CancellationToken cancellationToken = default)
    {
        var existingUser = await FindAsync(user => user.Name == request.Name, cancellationToken);

        if (existingUser == null)
            return default;

        var passwordCorrect = passwordVerifier.Invoke(existingUser);

        if (!passwordCorrect)
            return default;

        var (jwt, refreshToken) = _tokenService.GenerateTokenPair(existingUser, request.Fingerprint);

        existingUser.RefreshTokens.Add(refreshToken);

        existingUser.RefreshTokens.RemoveAll(t => !t.IsActive);

        await UpdateAsync(existingUser, cancellationToken);

        return new AuthenticateResponse(existingUser.Id, jwt, refreshToken);
    }

    public async Task<AuthenticateResponse?> Create(
        CreateUserRequest request,
        Expression<Func<Core.Entities.Authentication.User, bool>>? duplicatePredicate = default,
        CancellationToken cancellationToken = default)
    {
        var user = _mapper.Map<Core.Entities.Authentication.User>(request);

        var (jwt, refreshToken) = _tokenService.GenerateTokenPair(user, request.Fingerprint);
        
        user.RefreshTokens.Add(refreshToken);
            
        var createdUser = await AddAsync(user, duplicatePredicate, cancellationToken);

        return createdUser == default ? null : new AuthenticateResponse(createdUser.Id, jwt, refreshToken);
    }

    public async Task<AuthenticateResponse?> RefreshToken(
        RefreshTokenRequest request,
        CancellationToken cancellationToken = default)
    {
        var tokenOwner = await FindAsync(user => 
            user.RefreshTokens.Any(token => token.Body == request.Token), cancellationToken);

        if (tokenOwner == null)
            return default;

        var refreshToken = tokenOwner.RefreshTokens.Single(token => token.Body == request.Token);

        if (!refreshToken.IsActive || request.Fingerprint != refreshToken.Fingerprint)
            return default;

        tokenOwner.RefreshTokens.Remove(refreshToken);

        var (jwt, newRefreshToken) = _tokenService.GenerateTokenPair(tokenOwner, request.Fingerprint);
        
        tokenOwner.RefreshTokens.Add(newRefreshToken);

        await UpdateAsync(tokenOwner, cancellationToken);

        return new AuthenticateResponse(tokenOwner.Id, jwt, newRefreshToken);
    }

    public async Task<bool> RevokeToken(
        RevokeTokenRequest request,
        CancellationToken cancellationToken = default)
    {
        var tokenOwner = await FindAsync(user => 
            user.RefreshTokens.Any(token => token.Body == request.Token), cancellationToken);

        if (tokenOwner == null)
            return false;
        
        var refreshToken = tokenOwner.RefreshTokens.Single(token => token.Body == request.Token);

        if (!refreshToken.IsActive || request.Fingerprint != refreshToken.Fingerprint)
            return false;

        tokenOwner.RefreshTokens.Remove(refreshToken);

        await UpdateAsync(tokenOwner, cancellationToken);

        return true;
    }
}