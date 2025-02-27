//using Infrastructure.Authentication;
//using Infrastructure.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);

        //services.AddMyAuthentication();

        //services.AddMyAuthorization();

        return services;
    }
}