using Application;
using Infrastructure;
using Presentation;
using Infrastructure.Logger;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    MyAuthLoggerExtensions.CreateLogger(builder.Configuration);

    builder.Host.UseLogger();

    // Add services to the container.
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration)
        .AddPresentation();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    app.UseEarlyPresentation();

    app.UseInfrastructure();

    app.UseLatePresentation();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start");
}
finally
{
    Log.CloseAndFlush();
}
