using Application;
using MyAuth.Middlewares;
using Carter;
using Infrastructure;
using Presentation;
using Hangfire;
using Infrastructure.Notifications;
using Persistence.Cleaners;
using Infrastructure.Authentication;
using MyAuthBe.Cors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

builder.Services.AddMyAuthCors();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddPresentation();

var app = builder.Build();

app.UseMyAuthCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UsePersistenceCleaner();

app.UseMyAuthenticationAndAuthorization();

app.MapCarter();

app.UseHangfireDashboard();

app.MapHub<NotificationHub>("notification");

app.Run();
