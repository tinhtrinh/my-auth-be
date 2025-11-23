using Application.Shared.RealTime;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.RealTime;

public static class RealTimeExtensions
{
    public static IServiceCollection AddRealTimeService(this IServiceCollection services)
    {
        services.AddSignalR();

        services.AddTransient<IRealTimeService, RealTimeService>();

        return services;
    }

    public static WebApplication UseRealTimeService(this WebApplication app)
    {
        app.MapHub<RealTimeHub>("realTimeHub");

        return app;
    }
}
