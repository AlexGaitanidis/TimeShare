using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Domain.Aggregates.MeetingAggregate;

namespace TimeShare.Application.Meetings.Queries.GetMeetings;

public sealed record GetMeetingsQuery() : IQuery<IEnumerable<Meeting>>;