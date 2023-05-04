using TimeShare.Domain.Aggregates.HostAggregate;
using TimeShare.Domain.Aggregates.HostAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.UserAggregate.ValueObjects;

namespace TimeShare.Application.Abstractions.Persistence;

public interface IHostRepository : IRepository<Host, HostId>
{
    Task<Host?> GetByUserIdAsync(UserId userId, CancellationToken cancellationToken);

    Task<bool> HostExists(HostId hostId, CancellationToken cancellationToken);
}