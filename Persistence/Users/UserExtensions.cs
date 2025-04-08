using Application.Users;
using Application.Users.Delete;
using Application.Users.GetUsers;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Users.Delete;
using Persistence.Users.GetUsers;

namespace Persistence.Users;

internal static class UserExtensions
{
    public static IServiceCollection AddUserPersistence(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IGetUsersQueryService, GetUsersQueryService>();

        services.AddScoped<ISoftDeleteUserService, SoftDeleteUserService>();

        return services;
    }
}
