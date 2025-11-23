using Application.Shared.Background;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Shared.Background;

namespace Infrastructure.Backgrounds;

public static class BackgroundExtensions
{
    public static IServiceCollection AddBackgroundService(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddHangfire(options => options.UseSqlServerStorage(configuration.GetConnectionString("DevDB")));

        services.AddHangfireServer();

        services.AddTransient<IBackgroundService, BackgroundService>();

        services.AddTransient<IPersBackgroundService, PersBackgroundService>();

        return services;
    }

    public static WebApplication UseBackgroundService(this WebApplication app)
    {
        app.UseHangfireDashboard();

        return app;
    }
}
