using Microsoft.EntityFrameworkCore;
using TimeShare.Application.Abstractions.Persistence;
using TimeShare.Domain.Common.Primitives;

namespace TimeShare.Persistence.Repositories;

public class Repository<T, TId> : IRepository<T, TId>
    where T : AggregateRoot<TId>
    where TId : StronglyTypedId
{
    protected readonly TimeShareDbContext Context;

    public Repository(TimeShareDbContext context)
    {
        Context = context;
    }

    public async Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken)
    {
        return await Context.Set<T>().FirstOrDefaultAsync(ar => ar.Id == id, cancellationToken);
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await Context.Set<T>().ToListAsync(cancellationToken);
    }

    public void Add(T aggregateRoot)
    {
        Context.Set<T>().Add(aggregateRoot);
    }

    public void Update(T aggregateRoot)
    {
        Context.Set<T>().Update(aggregateRoot);
    }

    public void Delete(T aggregateRoot)
    {
        Context.Set<T>().Remove(aggregateRoot);
    }
}