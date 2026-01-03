using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Cors;

public static class CorsExtensions
{
    public static IServiceCollection AddMyAuthCors(this IServiceCollection services)
    {
        services.AddCors((options) =>
        {
            options.AddPolicy("AllowAny", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });

            options.AddPolicy("AllowAngular", policy =>
            {
                policy.WithOrigins("http://localhost:4200")
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });

        return services;
    }

    public static WebApplication UseMyAuthCors(this WebApplication app)
    {
        app.UseCors("AllowAngular");

        app.UseCors("AllowAny");

        return app;
    }
}
