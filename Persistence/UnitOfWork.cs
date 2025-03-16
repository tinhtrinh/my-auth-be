using Application.Abstractions;

namespace Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly MyAuthDbContext _dbContext;

    public UnitOfWork(MyAuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}