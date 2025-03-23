using Domain.Notification;

namespace Application.Notifications;

public interface INotificationRepository
{
    void Add(Notification notification);
}
