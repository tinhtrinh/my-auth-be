using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.Cors;
using Presentation.Middlewares;

namespace Presentation;

public static class PresentationExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        // add middleware hay service thứ tự như nào cũng được, vì nó chỉ là đăng ký service vào DI container, lúc cần là DI lấy ra chứ không cần biết thứ tự
        services.AddTransient<GlobalExceptionHandlingMiddleware>();

        services.AddMyAuthCors();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services.AddCarter(configurator: option => option.WithValidatorLifetime(ServiceLifetime.Scoped));

        return services;
    }

    public static WebApplication UseEarlyPresentation(this WebApplication app)
    {
        // phải use middlewares đúng thứ tự để pipeline thực thi đúng thứ tự trước sau
        app.UseMyAuthCors();

        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

        return app;
    }

    public static WebApplication UseLatePresentation(this WebApplication app)
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