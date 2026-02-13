using Application.Shared.Files;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Files;

public static class FileExtensions
{
    public static IServiceCollection AddFileService(this IServiceCollection services)
    {
        services.AddScoped<IFileStorageService, FileStorageService>();

        services.AddScoped<IFileService, FileService>();

        return services;
    }
}
