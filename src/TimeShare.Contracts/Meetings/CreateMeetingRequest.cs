namespace TimeShare.Contracts.Meetings;

public sealed record CreateMeetingRequest(
    string Name,
    string Description,
    DateTime StartOnUtc,
    DateTime EndOnUtc,
    int? MaxGuests,
    LocationRequest? Location);

public sealed record LocationRequest(
    string Name,
    string Address,
    double Latitude,
    double Longitude);