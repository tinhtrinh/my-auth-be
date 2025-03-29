using Application.Notifications;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Notification;

public class RealTimeNotifier : IRealTimeNotifier
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public RealTimeNotifier(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task Notify(Domain.Notification.Notification notification)
    {
        await _hubContext.Clients.All.SendAsync("SendNotification", notification);
    }
}
