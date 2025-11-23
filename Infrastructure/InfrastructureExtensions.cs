//using Infrastructure.Authorization;
using Infrastructure.Authentication;
using Infrastructure.Backgrounds;
using Infrastructure.Email;
using Infrastructure.Logger;
using Infrastructure.RealTime;
using Microsoft.AspNetCore.Builder;
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

        services.AddEmailService();

        services.AddMyAuthentication(configuration);

        //services.AddMyAuthorization();

        services.AddRealTimeService();

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseRequestLogging();

        app.UsePersistence();

        app.UseMyAuthenticationAndAuthorization();

        app.UseBackgroundService();

        app.UseRealTimeService();

        return app;
    }
}