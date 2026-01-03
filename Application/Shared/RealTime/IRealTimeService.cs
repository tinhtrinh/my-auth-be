namespace Application.Shared.RealTime;

public interface IRealTimeService
{
    Task SendAsync(string method, object? arg, CancellationToken cancellationToken = default);

    Task SendToUserAsync(string userId, string method, object? arg, CancellationToken cancellationToken = default);

    Task SendToConnectionAsync(string connectionId, string method, object? arg, CancellationToken cancellationToken = default);
}
