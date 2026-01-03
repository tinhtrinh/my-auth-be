using Application.Shared.Background;
using Hangfire;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Infrastructure.Backgrounds;

public class BackgroundService : IBackgroundService
{
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly ILogger<BackgroundService> _logger;

    public BackgroundService(IBackgroundJobClient backgroundJobClient, ILogger<BackgroundService> logger)
    {
        _backgroundJobClient = backgroundJobClient;
        _logger = logger;
    }

    public void Enqueue<T>(Expression<Func<T, Task>> methodCall)
    {
        try
        {
            _logger.LogInformation("Background service enqueues job");

            _backgroundJobClient.Enqueue(methodCall);

            _logger.LogInformation("Background service enqueues job successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to enqueue job");
            // tuỳ nhu cầu: có thể throw lại hoặc swallow lỗi
            throw; // nếu muốn propagate lỗi lên
        }
    }
}
