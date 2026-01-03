using Application.Shared.RealTime;
using Domain.Users;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Infrastructure.RealTime;

public class RealTimeService : IRealTimeService
{
    private readonly IHubContext<RealTimeHub> _hubContext;
    private readonly ILogger<RealTimeService> _logger;

    public RealTimeService(IHubContext<RealTimeHub> hubContext, ILogger<RealTimeService> logger)
    {
        _hubContext = hubContext;
        _logger = logger;
    }

    public async Task SendAsync(string method, object? arg, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("RealTimeService sends all");

            await _hubContext.Clients.All.SendAsync(method, arg, cancellationToken);

            _logger.LogInformation("RealTimeService sends all successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "RealTimeService failed to send all");
            throw;
        }
    }

    public async Task SendToUserAsync(string userId, string method, object? arg, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("RealTimeService sends to user");

            await _hubContext.Clients.User(userId).SendAsync(method, arg, cancellationToken);

            _logger.LogInformation("RealTimeService sends to user successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "RealTimeService failed to send to user");
            throw;
        }
    }

    public async Task SendToConnectionAsync(string connectionId, string method, object? arg, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("RealTimeService sends to connection");

            await _hubContext.Clients.Client(connectionId).SendAsync(method, arg, cancellationToken);

            _logger.LogInformation("RealTimeService sends to user successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "RealTimeService failed to send to user");
            throw;
        }
    }
}
