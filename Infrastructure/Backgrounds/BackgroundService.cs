using Application.Shared.Background;
using Hangfire;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Infrastructure.Backgrounds;

public class BackgroundService : IBackgroundService
{
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly ILogger<PersBackgroundService> _logger;

    public BackgroundService(IBackgroundJobClient backgroundJobClient, ILogger<PersBackgroundService> logger)
    {
        _backgroundJobClient = backgroundJobClient;
        _logger = logger;
    }

    public void Enqueue<TService>(Expression<Func<TService, Task>> task)
    {
        try
        {
            _logger.LogInformation("Background service enqueues job");

            _backgroundJobClient.Enqueue(task);

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
