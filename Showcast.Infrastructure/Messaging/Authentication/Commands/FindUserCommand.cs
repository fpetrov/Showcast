using MediatR;
using Showcast.Core.Entities.Authentication;
using Showcast.Core.Repositories.User;

namespace Showcast.Infrastructure.Messaging.Authentication.Commands;

public record FindUserCommand(string Name) : IRequest<User?>;

public class FindUserCommandHandler : IRequestHandler<FindUserCommand, User?>
{
    private readonly IUserRepository _userRepository;

    public FindUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> Handle(FindUserCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Name))
            return null;

        return await _userRepository.FindAsync(user => user.Name == request.Name, cancellationToken);
    }
}