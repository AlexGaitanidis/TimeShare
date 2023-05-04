using TimeShare.Domain.Aggregates.MeetingAggregate.ValueObjects;
using TimeShare.Domain.Common.Primitives;

namespace TimeShare.Domain.Aggregates.MeetingAggregate.Events;

public sealed record InvitationAccepted(Guid Id, InvitationId InvitationId, MeetingId MeetingId) : IDomainEvent;