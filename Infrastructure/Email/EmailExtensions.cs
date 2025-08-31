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
                .UseEmbeddedResourcesProject(typeof(InfrastructureExtensions))
                .SetOperatingAssembly(typeof(InfrastructureExtensions).Assembly)
                .UseMemoryCachingProvider()
                .Build();
        });

        return services;
    }
}
