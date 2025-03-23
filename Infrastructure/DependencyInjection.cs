//using Infrastructure.Authentication;
//using Infrastructure.Authorization;
using Application.Abstractions;
using Application.Notifications;
using Hangfire;
using Infrastructure.BackgroundService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);

        services.AddHangfire(options => options.UseSqlServerStorage(configuration.GetConnectionString("DevDB")));

        services.AddHangfireServer();

        services.AddScoped<INotificationService, NotificationService.NotificationService>();

        //services.AddMyAuthentication();

        //services.AddMyAuthorization();

        return services;
    }
}