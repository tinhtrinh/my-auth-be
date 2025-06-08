using Application.Shared.Email;
using Microsoft.Extensions.DependencyInjection;
using RazorLight;
namespace Infrastructure.Email;

public static class EmailExtensions
{
    public static IServiceCollection AddEmailService(this IServiceCollection services)
    {
        services.AddTransient<IEmailService, EmailService>();

        services.AddTransient<IRazorLightEngine>(sp =>
        {
            return new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(typeof(DependencyInjection))
                .SetOperatingAssembly(typeof(DependencyInjection).Assembly)
                .UseMemoryCachingProvider()
                .Build();
        });

        return services;
    }
}
