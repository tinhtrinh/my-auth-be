using Application.Behaviors;
using Application.Notifications.NotificationService;
using Application.Shared.Notifications;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ApplicationExtensions).Assembly;

        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));

        services.AddTransient<INotificationService, NotificationService>();

        return services;
    }
}