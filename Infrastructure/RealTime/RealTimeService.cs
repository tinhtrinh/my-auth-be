using Application.Shared.RealTime;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.RealTime;

public class RealTimeService : IRealTimeService
{
    private readonly IHubContext<RealTimeHub> _hubContext;

    public RealTimeService(IHubContext<RealTimeHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendAsync(string method, object? arg, CancellationToken cancellationToken = default)
    {
        await _hubContext.Clients.All.SendAsync(method, arg, cancellationToken);
    }
}
