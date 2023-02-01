using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Showcast.Core.Entities.Authentication;
using Showcast.Core.Repositories.User;
using Showcast.Core.Services.Security;
using Showcast.Infrastructure.Repositories.User;
using Showcast.Infrastructure.Services.Security;

namespace Showcast.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDefaultAuthentication(this IServiceCollection services,
        Action<AuthenticationOptions>? options = default)
    {
        options ??= _ => { _ = AuthenticationOptions.Default; };

        services.Configure(options);

        services.AddScoped<IHashService, HashService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserRepository, UserRepository>();

        services
            .AddAuthentication(configure =>
            {
                configure.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                configure.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(configure => ValidateJwtOptions(options, configure));

        return services;
    }

    private static void ValidateJwtOptions(Action<AuthenticationOptions> authOptions, JwtBearerOptions options)
    {
        var authenticationOptions = AuthenticationOptions.Default;
        
        authOptions.Invoke(authenticationOptions);
        
        var serverSecret =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationOptions.SecurityKey));

        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = serverSecret,
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidIssuer = authenticationOptions.Issuer,
            ValidateAudience = true,
            ClockSkew = TimeSpan.Zero,
            ValidAudience = authenticationOptions.Audience,
            ValidateLifetime = true
        };
    }
}