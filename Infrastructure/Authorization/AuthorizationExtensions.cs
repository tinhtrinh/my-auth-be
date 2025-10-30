using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Authorization;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddMyAuthorization(this IServiceCollection services)
    {

        services.AddAuthorization();

        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        return services;
    }
}