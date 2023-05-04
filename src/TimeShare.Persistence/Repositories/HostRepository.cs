using Microsoft.EntityFrameworkCore;
using TimeShare.Application.Abstractions.Persistence;
using TimeShare.Domain.Aggregates.HostAggregate;
using TimeShare.Domain.Aggregates.HostAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.UserAggregate.ValueObjects;

namespace TimeShare.Persistence.Repositories;

public class HostRepository : Repository<Host, HostId>, IHostRepository
{
    public HostRepository(TimeShareDbContext context) : base(context)
    {
    }

    public async Task<Host?> GetByUserIdAsync(UserId userId, CancellationToken cancellationToken)
    {
        return await Context.Hosts.FirstOrDefaultAsync(g => g.UserId == userId, cancellationToken);
    }

    public async Task<bool> HostExists(HostId hostId, CancellationToken cancellationToken)
    {
        return await Context.Hosts.AnyAsync(h => h.Id == hostId, cancellationToken);
    }
}