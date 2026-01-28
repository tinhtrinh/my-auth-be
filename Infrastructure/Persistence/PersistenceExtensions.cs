using Application.Shared.UnitOfWork;
using Infrastructure.Persistence.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Notifications;
using Persistence.Shared.Cleaner;
using Persistence.UnitOfWorks;
using Persistence.Users;

namespace Infrastructure.Persistence;

public static class PersistenceExtensions
{
    public static IServiceCollection AddNewPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MyAuthDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DevDB"))
        );

        //services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddUserPersistence();

        //services.AddNotificationPersistence();

        //services.AddCleaner();

        return services;
    }

    public static WebApplication UseNewPersistence(this WebApplication app)
    {
        //app.UseCleaner();

        return app;
    }
}