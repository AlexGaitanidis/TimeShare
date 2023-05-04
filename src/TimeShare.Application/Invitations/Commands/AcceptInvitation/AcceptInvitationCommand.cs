using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Domain.Aggregates.MeetingAggregate.Entities;
using TimeShare.Domain.Aggregates.MeetingAggregate.ValueObjects;

namespace TimeShare.Application.Invitations.Commands.AcceptInvitation;

public sealed record AcceptInvitationCommand(MeetingId MeetingId, InvitationId InvitationId) : ICommand<Invitation>;