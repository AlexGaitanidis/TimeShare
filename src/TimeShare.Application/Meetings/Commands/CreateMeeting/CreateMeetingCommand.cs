using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Domain.Aggregates.HostAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.MeetingAggregate;

namespace TimeShare.Application.Meetings.Commands.CreateMeeting;

public sealed record CreateMeetingCommand(
    string Name,
    string Description,
    DateTime StartOnUtc,
    DateTime EndOnUtc,
    int? MaxGuests,
    HostId HostId,
    LocationCommand? Location) : ICommand<Meeting>;

public sealed record LocationCommand(
    string Name,
    string Address,
    double Latitude,
    double Longitude);