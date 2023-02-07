using AutoMapper;
using MediatR;
using Showcast.Core.Messaging.Requests.Authentication;
using Showcast.Core.Messaging.Responses.Authentication;
using Showcast.Core.Repositories.User;

namespace Showcast.Infrastructure.Messaging.Authentication.Commands;

public record RefreshTokenCommand(string? Token, string Fingerprint, long TelegramId = 0) : IRequest<AuthenticateResponse>;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthenticateResponse?>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public RefreshTokenCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<AuthenticateResponse?> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshTokenRequestRequest = _mapper.Map<RefreshTokenRequest>(request);

        var response = await _userRepository
            .RefreshTokenAsync(refreshTokenRequestRequest, cancellationToken);

        return response;
    }
}