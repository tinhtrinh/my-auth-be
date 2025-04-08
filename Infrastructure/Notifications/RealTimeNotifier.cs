using Application.Notifications;
using Domain.Notifications;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Notifications;

public class RealTimeNotifier : IRealTimeNotifier
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public RealTimeNotifier(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task Notify(Notification notification)
    {
        await _hubContext.Clients.All.SendAsync("SendNotification", notification);
    }
}
