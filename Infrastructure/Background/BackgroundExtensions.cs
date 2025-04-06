using Application.Abstractions;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Background;

public static class BackgroundExtensions
{
    public static IServiceCollection AddBackgroundService(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddHangfire(options => options.UseSqlServerStorage(configuration.GetConnectionString("DevDB")));

        services.AddHangfireServer();

        services.AddTransient<IBackgroundService, BackgroundService>();

        return services;
    }
}
