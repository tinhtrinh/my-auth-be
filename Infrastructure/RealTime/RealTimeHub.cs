using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.RealTime;

[Authorize]
public class RealTimeHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var connectionId = Context.ConnectionId;
        await Clients.Caller.SendAsync("ReceiveConnectionId", connectionId);
        await base.OnConnectedAsync();
    }
}
