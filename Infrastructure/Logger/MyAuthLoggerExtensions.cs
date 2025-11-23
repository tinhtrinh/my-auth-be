using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Infrastructure.Logger;

public static class MyAuthLoggerExtensions
{
    public static void CreateLogger(IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }

    public static IHostBuilder UseLogger(this IHostBuilder host)
    {
        host.UseSerilog();

        return host;
    }

    public static WebApplication UseRequestLogging(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        return app;
    }
}
