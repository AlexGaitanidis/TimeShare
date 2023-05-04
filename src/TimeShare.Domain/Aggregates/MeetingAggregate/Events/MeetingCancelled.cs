using TimeShare.Domain.Aggregates.MeetingAggregate.ValueObjects;
using TimeShare.Domain.Common.Primitives;

namespace TimeShare.Domain.Aggregates.MeetingAggregate.Events;

public record MeetingCancelled(Guid Id, MeetingId MeetingId) : IDomainEvent;