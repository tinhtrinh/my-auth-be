using Domain.Notifications;

namespace Application.Shared.Notifications;

public interface INotificationService
{
    Task SendAsync(Notification notification, CancellationToken cancellationToken = default);
}
