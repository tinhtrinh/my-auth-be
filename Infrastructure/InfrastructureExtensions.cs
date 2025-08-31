//using Infrastructure.Authorization;
using Application.Notifications;
using Infrastructure.Authentication;
using Infrastructure.Backgrounds;
using Infrastructure.Email;
using Infrastructure.Notifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

namespace Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);

        services.AddBackgroundService(configuration);

        services.AddSignalR();

        services.AddTransient<IRealTimeNotifier, RealTimeNotifier>();

        services.AddEmailService();

        services.AddMyAuthentication();

        //services.AddMyAuthorization();

        return services;
    }
}