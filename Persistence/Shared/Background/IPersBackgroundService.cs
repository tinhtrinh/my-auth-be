using System.Linq.Expressions;

namespace Persistence.Shared.Background;

public interface IPersBackgroundService
{
    void Schedule<TJob>(string jobName, Expression<Func<TJob, Task>> jobAction, string cron);
}
