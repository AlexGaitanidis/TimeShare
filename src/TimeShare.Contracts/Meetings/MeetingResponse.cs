using TimeShare.Contracts.Common;

namespace TimeShare.Contracts.Meetings;

public record MeetingResponse(
    Guid Id,
    string Name,
    string Description,
    AverageRatingResponse AverageRating,
    DateTime StartOnUtc,
    DateTime EndOnUtc,
    int? MaxGuests,
    int GuestCount,
    Guid HostId,
    LocationResponse? Location,
    string Status,
    List<InvitationResponse> Invitations,
    //List<Guid> MeetingReviewIds,
    DateTime CreatedOnUtc,
    DateTime? ModifiedOnUtc);

public sealed record LocationResponse(
    string Name,
    string Address,
    double Latitude,
    double Longitude);

public sealed record InvitationResponse(
    Guid Id,
    Guid MeetingId,
    Guid GuestId,
    string Status);