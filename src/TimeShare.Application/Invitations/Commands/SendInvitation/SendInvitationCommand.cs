using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Domain.Aggregates.GuestAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.MeetingAggregate.Entities;
using TimeShare.Domain.Aggregates.MeetingAggregate.ValueObjects;

namespace TimeShare.Application.Invitations.Commands.SendInvitation;

public sealed record SendInvitationCommand(MeetingId MeetingId, GuestId GuestId) : ICommand<Invitation>;