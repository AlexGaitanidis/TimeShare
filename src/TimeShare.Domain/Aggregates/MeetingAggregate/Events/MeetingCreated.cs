using TimeShare.Domain.Aggregates.HostAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.MeetingAggregate.ValueObjects;
using TimeShare.Domain.Common.Primitives;

namespace TimeShare.Domain.Aggregates.MeetingAggregate.Events;

public sealed record MeetingCreated(Guid Id, MeetingId MeetingId, HostId HostId) : IDomainEvent;