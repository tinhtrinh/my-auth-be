using Application.Shared.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddMyAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();

        var idpSettings = configuration.GetSection("IdentityProviders:Keycloak");
        var authority = idpSettings["Authority"];
        var clientId = idpSettings["ClientId"];
        var clientSecret = idpSettings["ClientSecret"];
        var audience = idpSettings["Audience"];
        var requireHttps = bool.Parse(idpSettings["RequireHttpsMetadata"] ?? "false");

        // Thêm xác thực JWT
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = authority;
                options.Audience = audience;
                options.RequireHttpsMetadata = requireHttps;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateIssuer = true,
                    ValidIssuer = authority,
                    ValidateLifetime = true
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        Console.WriteLine($"Challenge triggered: {context.Error}, {context.ErrorDescription}");
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddAuthorization();

        services.AddScoped<IJwtProvider, JwtProvider>();

        return services;
    }

    public static WebApplication UseMyAuthenticationAndAuthorization(this WebApplication app)
    {
        app.UseAuthentication();

        app.UseAuthorization();

        return app;
    }
}