using Microsoft.EntityFrameworkCore;
using TimeShare.Application.Abstractions.Persistence;
using TimeShare.Domain.Aggregates.HostAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.MeetingAggregate;
using TimeShare.Domain.Aggregates.MeetingAggregate.ValueObjects;

namespace TimeShare.Persistence.Repositories;

public class MeetingRepository : Repository<Meeting, MeetingId>, IMeetingRepository
{
    public MeetingRepository(TimeShareDbContext context) : base(context)
    {
    }

    public async Task<List<Meeting>> GetByHostAsync(HostId hostId, CancellationToken cancellationToken)
    {
        return await Context.Meetings.Where(m => m.HostId == hostId).ToListAsync(cancellationToken);
    }
}