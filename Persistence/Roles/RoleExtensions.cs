using Microsoft.Extensions.DependencyInjection;

namespace Persistence.Roles;

public static class RoleExtensions
{
    public static IServiceCollection AddRolePersistence(this IServiceCollection services)
    {
        //services.AddScoped<IRoleRepository, RoleRepository>();

        return services;
    }
}
