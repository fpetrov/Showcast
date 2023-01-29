using AutoMapper;
using MediatR;
using Showcast.Core.Messaging.Requests.Authentication;
using Showcast.Core.Repositories.User;

namespace Showcast.Infrastructure.Messaging.Authentication.Commands;

public record RevokeTokenCommand(string? Token, string Fingerprint) : IRequest<bool>;

public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public RevokeTokenCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshTokenRequestRequest = _mapper.Map<RevokeTokenRequest>(request);

        var response = await _userRepository
            .RevokeToken(refreshTokenRequestRequest, cancellationToken);

        return response;
    }
}