//using Domain.Roles;
using Domain.Shared;
using Domain.UserRules;

using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using Persistence.Roles;
using Persistence.Shared;
using Persistence.UserRules;
using Persistence.Users;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MyAuthDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DevDB"))
        );

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUserRuleRepository, UserRuleRepository>();

        //services.AddScoped<IRoleRepository, RoleRepository>();

        return services;
    }
}