using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Shared.Background;

namespace Persistence.Shared.Cleaner;

public static class CleanerExtensions
{
    public static IServiceCollection AddCleaner(this IServiceCollection services)
    {
        var cleanerType = typeof(ICleaner);
        var assembly = typeof(CleanerExtensions).Assembly;
        var cleaners = assembly.GetTypes()
            .Where(t => cleanerType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);
        foreach (var cleaner in cleaners)
        {
            services.AddTransient(cleanerType, cleaner);
        }

        services.AddTransient<ICollection<ICleaner>>(provider =>
            provider.GetServices<ICleaner>().ToList());

        services.AddTransient<ICleanerJob, CleanerJob>();

        return services;
    }

    public static WebApplication UseCleaner(this WebApplication app)
    {
        app.Services
            .GetRequiredService<IPersBackgroundService>()
            .Schedule<ICleanerJob>
            (
                "deleted-records-cleaner",
                job => job.CleanAsync(),
                "0/15 * * * * *");

        return app;
    }
}
