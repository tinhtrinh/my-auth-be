using Domain.Notifications;

namespace Application.Shared;

public interface IAddNotificationService
{
    Task AddNotification(Notification notification, CancellationToken token);
}
