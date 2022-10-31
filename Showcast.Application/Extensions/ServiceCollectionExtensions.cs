namespace Showcast.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUsersRepository(this IServiceCollection services)
    {
        // TODO
        services.AddScoped(typeof(object));

        return services;
    }

    public static IServiceCollection AddSecurityRepository(this IServiceCollection services)
    {
        // TODO
        services.AddScoped(typeof(object));

        return services;
    }
}