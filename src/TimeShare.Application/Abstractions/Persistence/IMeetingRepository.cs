using TimeShare.Domain.Aggregates.HostAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.MeetingAggregate;
using TimeShare.Domain.Aggregates.MeetingAggregate.ValueObjects;

namespace TimeShare.Application.Abstractions.Persistence;

public interface IMeetingRepository : IRepository<Meeting, MeetingId>
{
    Task<List<Meeting>> GetByHostAsync(HostId hostId, CancellationToken cancellationToken);
}