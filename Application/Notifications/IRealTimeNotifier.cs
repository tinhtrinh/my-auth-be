using Domain.Notification;

namespace Application.Notifications;

public interface IRealTimeNotifier
{
    Task Notify(Notification notification);
}
