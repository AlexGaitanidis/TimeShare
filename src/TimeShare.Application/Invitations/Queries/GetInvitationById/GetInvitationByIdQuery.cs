using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Domain.Aggregates.MeetingAggregate.Entities;
using TimeShare.Domain.Aggregates.MeetingAggregate.ValueObjects;

namespace TimeShare.Application.Invitations.Queries.GetInvitationById;

public sealed record GetInvitationByIdQuery(MeetingId MeetingId, InvitationId InvitationId) : IQuery<Invitation>;