using Application;
using MyAuth.Middlewares;
using Infrastructure;
using Presentation;
using MyAuthBe.Cors;
using Infrastructure.Logger;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    MyAuthLoggerExtensions.CreateLogger(builder.Configuration);

    builder.Host.UseLogger();

    // Add services to the container.
    builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

    builder.Services.AddMyAuthCors();

    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration)
        .AddPresentation();

    var app = builder.Build();

    app.UseMyAuthCors();

    // Configure the HTTP request pipeline.

    app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

    app.UseInfrastructure();

    app.UsePresentation();

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
