using Application.Users;
using Application.Users.Delete;
using Application.Users.GetUsers;
using Application.Users.ListView;
using Application.Users.Update;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Users.Delete;
using Persistence.Users.GetUsers;
using Persistence.Users.ListView;
using Persistence.Users.Update;

namespace Persistence.Users;

internal static class UserExtensions
{
    public static IServiceCollection AddUserPersistence(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IGetUsersQueryService, GetUsersQueryService>();

        services.AddScoped<ISoftDeleteUserService, SoftDeleteUserService>();

        services.AddScoped<IUpdateUserFinder, UpdateUserFinder>();

        services.AddScoped<IGetUserListService, GetUserListService>();

        return services;
    }
}
