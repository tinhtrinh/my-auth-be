using Application.Shared.Files;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Files;

public static class FileExtensions
{
    public static IServiceCollection AddFileService(this IServiceCollection services)
    {
        services.AddScoped<IFileStorageService, FileStorageService>();

        return services;
    }
}
