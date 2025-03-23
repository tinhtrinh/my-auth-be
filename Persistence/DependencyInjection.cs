using Application.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Notifications;
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

        services.AddScoped<Application.Abstractions.IUnitOfWork, UnitOfWork>();

        services.AddUserPersistence();

        services.AddScoped<INotificationRepository, NotificationRepository>();

        //services.AddScoped<IUserRuleRepository, UserRuleRepository>();

        //services.AddScoped<IRoleRepository, RoleRepository>();

        return services;
    }
}