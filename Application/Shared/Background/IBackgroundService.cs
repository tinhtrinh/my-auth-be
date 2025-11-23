using System.Linq.Expressions;

namespace Application.Shared.Background;

public interface IBackgroundService
{
    void Enqueue<TService>(Expression<Func<TService, Task>> task);
}
