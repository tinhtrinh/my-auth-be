using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Presentation;

public static class PresentationExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services.AddCarter(configurator: option => option.WithValidatorLifetime(ServiceLifetime.Scoped));

        return services;
    }

    public static WebApplication UsePresentation(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapCarter();

        return app;
    }
}