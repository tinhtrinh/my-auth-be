using Application.Notifications;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.Notifications;

public static class NotificationExtensions
{
    public static IServiceCollection AddNotificationPersistence(this IServiceCollection services)
    {
        services.AddScoped<INotificationRepository, NotificationRepository>();

        return services;
    }
}
