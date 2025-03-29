using System.Linq.Expressions;

namespace Application.Abstractions;

public interface IBackgroundService
{
    void Enqueue<TService>(Expression<Func<TService, Task>> task);
}
