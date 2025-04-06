//using Infrastructure.Authentication;
//using Infrastructure.Authorization;
using Application.Abstractions;
using Application.Notifications;
using Hangfire;
using Infrastructure.Background;
using Infrastructure.Notification;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);

        services.AddBackgroundService(configuration);

        services.AddSignalR();

        services.AddTransient<IRealTimeNotifier, RealTimeNotifier>();

        //services.AddMyAuthentication();

        //services.AddMyAuthorization();

        return services;
    }
}