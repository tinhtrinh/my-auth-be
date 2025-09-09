using Application.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Authentication;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddMyAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

        services.ConfigureOptions<JwtOptionsSetup>();

        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.AddScoped<IJwtProvider, JwtProvider>();

        return services;
    }
}