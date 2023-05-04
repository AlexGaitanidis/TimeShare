using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Domain.Aggregates.MeetingAggregate.Entities;
using TimeShare.Domain.Aggregates.MeetingAggregate.ValueObjects;

namespace TimeShare.Application.Invitations.Queries.GetInvitationsByMeeting;

public sealed record GetInvitationsByMeetingQuery(MeetingId MeetingId) : IQuery<IEnumerable<Invitation>>;