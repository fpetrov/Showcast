using AutoMapper;
using MediatR;
using Showcast.Core.Messaging.Requests.Authentication;
using Showcast.Core.Messaging.Responses.Authentication;
using Showcast.Core.Repositories.User;
using Showcast.Core.Services.Security;

namespace Showcast.Infrastructure.Messaging.Authentication.Commands;

public record AuthenticateCommand(string Name, string Password, string Fingerprint) : IRequest<AuthenticateResponse>;

public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, AuthenticateResponse?>
{
    private readonly IUserRepository _userRepository;
    private readonly IHashService _hashService;
    private readonly IMapper _mapper;

    public AuthenticateCommandHandler(IUserRepository userRepository, IHashService hashService, IMapper mapper)
    {
        _userRepository = userRepository;
        _hashService = hashService;
        _mapper = mapper;
    }

    public async Task<AuthenticateResponse?> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        var authenticateRequest = _mapper.Map<AuthenticateRequest>(request);

        var response = await _userRepository
            .Authenticate(authenticateRequest, user =>
                _hashService.Verify(authenticateRequest.Password, user.Password), cancellationToken);

        return response;
    }
}