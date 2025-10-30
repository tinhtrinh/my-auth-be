namespace MyAuthBe.Cors;

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

    public static IApplicationBuilder UseMyAuthCors(this IApplicationBuilder app)
    {
        app.UseCors("AllowAngular");

        app.UseCors("AllowAny");

        return app;
    }
}
