using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Domain.Aggregates.MeetingAggregate;
using TimeShare.Domain.Aggregates.MeetingAggregate.ValueObjects;

namespace TimeShare.Application.Meetings.Queries.GetMeetingById;

public sealed record GetMeetingByIdQuery(MeetingId MeetingId) : IQuery<Meeting>;