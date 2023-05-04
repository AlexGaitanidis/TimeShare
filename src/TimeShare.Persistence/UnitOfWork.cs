using TimeShare.Application.Abstractions.Persistence;

namespace TimeShare.Persistence;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly TimeShareDbContext _dbContext;

    public UnitOfWork(TimeShareDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}