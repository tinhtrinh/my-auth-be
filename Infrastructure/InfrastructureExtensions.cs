//using Infrastructure.Authorization;
using Infrastructure.Authentication;
using Infrastructure.Authorization;
using Infrastructure.Backgrounds;
using Infrastructure.Email;
using Infrastructure.Export;
using Infrastructure.Logger;
using Infrastructure.Persistence;
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
        services.AddNewPersistence(configuration);

        services.AddPersistence(configuration);

        services.AddBackgroundService(configuration);

        services.AddEmailService();

        services.AddMyAuthentication(configuration);

        services.AddMyAuthorization();

        services.AddRealTimeService();

        services.AddExportService();

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseMyAuthenticationAndAuthorization();

        app.UseRequestLogging();

        app.UsePersistence();

        app.UseBackgroundService();

        app.UseRealTimeService();

        return app;
    }
}