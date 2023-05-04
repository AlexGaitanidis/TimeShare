using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Domain.Aggregates.HostAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.MeetingAggregate;

namespace TimeShare.Application.Meetings.Queries.GetMeetingsByHost;

public sealed record GetMeetingsByHostQuery(HostId HostId) : IQuery<IEnumerable<Meeting>>;