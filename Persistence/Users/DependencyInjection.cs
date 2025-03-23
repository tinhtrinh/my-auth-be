using Application.Users.GetUsers;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Users.GetUsers;
namespace Persistence.Users;

internal static class DependencyInjection
{
    public static IServiceCollection AddUserPersistence(this IServiceCollection services)
    {
        services.AddScoped<Application.Users.IUserRepository, UserRepository>();

        services.AddScoped<IGetUsersQueryService, GetUsersQueryService>();

        return services;
    }
}
