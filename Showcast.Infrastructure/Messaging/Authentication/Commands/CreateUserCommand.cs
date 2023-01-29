using AutoMapper;
using MediatR;
using Showcast.Core.Messaging.Requests.Authentication;
using Showcast.Core.Messaging.Responses.Authentication;
using Showcast.Core.Repositories.User;
using Showcast.Core.Services.Security;

namespace Showcast.Infrastructure.Messaging.Authentication.Commands;

public record CreateUserCommand(string Name, string Password, string Fingerprint) : IRequest<AuthenticateResponse>;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, AuthenticateResponse?>
{
    private readonly IUserRepository _userRepository;
    private readonly IHashService _hashService;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUserRepository userRepository, IHashService hashService, IMapper mapper)
    {
        _userRepository = userRepository;
        _hashService = hashService;
        _mapper = mapper;
    }

    public async Task<AuthenticateResponse?> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var createUserRequest = _mapper.Map<CreateUserRequest>(request) 
            with
            {
                Password = _hashService.Hash(request.Password)
            };
        
        var response = await _userRepository.Create(
            createUserRequest,
            user => user.Name == createUserRequest.Name,
            cancellationToken);

        return response;
    }
}