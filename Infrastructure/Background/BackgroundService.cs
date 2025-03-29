using Application.Abstractions;
using Hangfire;
using System.Linq.Expressions;

namespace Infrastructure.Background;

public class BackgroundService : IBackgroundService
{
    private readonly IBackgroundJobClient _backgroundJobClient;

    public BackgroundService(IBackgroundJobClient backgroundJobClient)
    {
        _backgroundJobClient = backgroundJobClient;
    }

    public void Enqueue<TService>(Expression<Func<TService, Task>> task)
    {
        _backgroundJobClient.Enqueue(task);
    }
}
