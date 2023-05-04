using TimeShare.Domain.Common.Primitives;

namespace TimeShare.Application.Abstractions.Persistence;

public interface IRepository<T, TId>
    where T : AggregateRoot<TId>
    where TId : StronglyTypedId
{
    Task<T?> GetByIdAsync(TId id, CancellationToken cancellationToken);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
    void Add(T aggregateRoot);
    void Update(T aggregateRoot);
    void Delete(T aggregateRoot);
}