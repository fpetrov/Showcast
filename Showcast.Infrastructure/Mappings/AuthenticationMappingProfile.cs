using AutoMapper;
using Showcast.Core.Entities.Authentication;
using Showcast.Core.Messaging.Requests.Authentication;
using Showcast.Infrastructure.Messaging.Authentication.Commands;

namespace Showcast.Infrastructure.Mappings;

public class AuthenticationMappingProfile : Profile
{
    public AuthenticationMappingProfile()
    {
        CreateMap<CreateUserRequest, User>();
        
        CreateMap<AuthenticateCommand, AuthenticateRequest>();
        CreateMap<CreateUserCommand, CreateUserRequest>();
        CreateMap<RefreshTokenCommand, RefreshTokenRequest>();
        CreateMap<RevokeTokenCommand, RevokeTokenRequest>();
    }
}