using TimeShare.Domain.Aggregates.MeetingAggregate.ValueObjects;
using TimeShare.Domain.Common.Primitives;

namespace TimeShare.Domain.Aggregates.MeetingAggregate.Events;

public sealed record InvitationSent(Guid Id, InvitationId InvitationId) : IDomainEvent;