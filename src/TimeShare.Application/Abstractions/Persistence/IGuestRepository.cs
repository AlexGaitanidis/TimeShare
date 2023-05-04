using TimeShare.Domain.Aggregates.GuestAggregate;
using TimeShare.Domain.Aggregates.GuestAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.UserAggregate.ValueObjects;

namespace TimeShare.Application.Abstractions.Persistence;

public interface IGuestRepository : IRepository<Guest, GuestId>
{
    Task<Guest?> GetByUserIdAsync(UserId userId, CancellationToken cancellationToken);
}