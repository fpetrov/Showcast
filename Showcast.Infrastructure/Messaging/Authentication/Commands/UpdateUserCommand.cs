using MediatR;
using Showcast.Core.Entities.Authentication;
using Showcast.Core.Repositories.User;

namespace Showcast.Infrastructure.Messaging.Authentication.Commands;

public record UpdateUserCommand(User User) : IRequest<bool>;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.UpdateAsync(request.User, cancellationToken);
        
        return true;
    }
}