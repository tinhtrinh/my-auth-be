using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.Cleaners;

public static class CleanerExtensions
{
    public static IServiceCollection AddCleaner(this IServiceCollection services)
    {
        AddDeletedRecordsCleaners(services);

        return services;
    }

    public static IApplicationBuilder UsePersistenceCleaner(this WebApplication app)
    {
        UseDeletedRecordsCleaner(app);

        return app;
    }

    private static void AddDeletedRecordsCleaners(IServiceCollection services)
    {
        var cleanerType = typeof(IDeletedRecordsCleaner);
        var assembly = typeof(CleanerExtensions).Assembly;
        var cleaners = assembly.GetTypes()
            .Where(t => cleanerType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);
        foreach (var cleaner in cleaners)
        {
            services.AddTransient(cleanerType, cleaner);
        }

        services.AddTransient<ICollection<IDeletedRecordsCleaner>>(provider =>
            provider.GetServices<IDeletedRecordsCleaner>().ToList());

        services.AddTransient<IDeletedRecordsCleanerJob, DeletedRecordsCleanerJob>();
    }

    private static void UseDeletedRecordsCleaner(WebApplication app)
    {
        app.Services
            .GetRequiredService<IRecurringJobManager>()
            .AddOrUpdate
            <IDeletedRecordsCleanerJob>
            (
                "deleted-records-cleaner",
                job => job.CleanAsync(),
                "0/1 * * * * *");
    }
}
