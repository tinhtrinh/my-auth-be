using Domain.Notifications;

namespace Application.Notifications;

public interface INotificationRepository
{
    void Add(Notification notification);
}
