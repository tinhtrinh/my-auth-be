namespace Application.Shared.RealTime;

public interface IRealTimeService
{
    Task SendAsync(string method, object? arg, CancellationToken cancellationToken = default);

    // hiện tại là send all
    // nhưng sau này sẽ nâng cấp cho SendAsync nhận thêm connectId để send tới duy nhất connection đó
}
