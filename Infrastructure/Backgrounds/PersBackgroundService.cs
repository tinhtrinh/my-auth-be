using Hangfire;
using Microsoft.Extensions.Logging;
using Persistence.Shared.Background;
using System.Linq.Expressions;

namespace Infrastructure.Backgrounds;

public class PersBackgroundService : IPersBackgroundService
{
    private readonly IRecurringJobManager _manager;
    private readonly ILogger<PersBackgroundService> _logger;

    public PersBackgroundService(IRecurringJobManager manager, ILogger<PersBackgroundService> logger)
    {
        _manager = manager;
        _logger = logger;
    }

    public void Schedule<TJob>(string jobName, Expression<Func<TJob, Task>> jobAction, string cron)
    {
        try
        {
            _logger.LogInformation("Scheduling job {JobName} with cron {Cron}", jobName, cron);

            _manager.AddOrUpdate(jobName, jobAction, cron);

            _logger.LogInformation("Job {JobName} has been scheduled successfully.", jobName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to schedule job {JobName} with cron {Cron}", jobName, cron);
            // tuỳ nhu cầu: có thể throw lại hoặc swallow lỗi
            throw; // nếu muốn propagate lỗi lên
        }
    }
}
