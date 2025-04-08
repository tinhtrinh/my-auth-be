using Domain.Notifications;

namespace Application.Notifications;

public interface IRealTimeNotifier
{
    Task Notify(Notification notification);
}
