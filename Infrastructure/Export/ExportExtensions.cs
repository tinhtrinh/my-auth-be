using Application.Shared.Export;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Export;

public static class ExportExtensions
{
    public static IServiceCollection AddExportService(this IServiceCollection services)
    {
        services.AddTransient<IExportService, ExportService>();

        return services;
    }
}
