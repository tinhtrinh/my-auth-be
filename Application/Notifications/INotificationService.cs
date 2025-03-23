using Domain.Notification;

namespace Application.Notifications;

public interface INotificationService
{
    public Task SendNotification(Notification notification, CancellationToken cancellationToken);
}
