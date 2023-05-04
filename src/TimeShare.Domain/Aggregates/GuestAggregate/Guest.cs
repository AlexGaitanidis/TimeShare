using TimeShare.Domain.Aggregates.GuestAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.MeetingAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.MeetingReviewAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.UserAggregate;
using TimeShare.Domain.Aggregates.UserAggregate.ValueObjects;
using TimeShare.Domain.Common.Primitives;
using TimeShare.Domain.Common.ValueObjects;

namespace TimeShare.Domain.Aggregates.GuestAggregate;

public sealed class Guest : AggregateRoot<GuestId>
{
    private readonly List<MeetingId> _upcomingMeetingIds = new();
    private readonly List<MeetingId> _pastMeetingIds = new();
    private readonly List<MeetingId> _pendingMeetingIds = new();
    private readonly List<MeetingReviewId> _meetingReviewIds = new();

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public AverageRating AverageRating { get; private set; }
    public UserId UserId { get; private set; }

    public IReadOnlyList<MeetingId> UpcomingMeetingIds => _upcomingMeetingIds.AsReadOnly();
    public IReadOnlyList<MeetingId> PastMeetingIds => _pastMeetingIds.AsReadOnly();
    public IReadOnlyList<MeetingId> PendingMeetingIds => _pendingMeetingIds.AsReadOnly();
    public IReadOnlyList<MeetingReviewId> MeetingReviewIds => _meetingReviewIds.AsReadOnly();

    private Guest(GuestId id, string firstName, string lastName, AverageRating averageRating, UserId userId)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        AverageRating = averageRating;
        UserId = userId;
    }

#pragma warning disable CS8618
    private Guest() { }
#pragma warning restore CS8618

    public static Guest Create(User user)
    {
        return new Guest(GuestId.CreateUnique(), user.FirstName, user.LastName, AverageRating.CreateNew(), user.Id);
    }

    internal void ReceiveInvitation(MeetingId meetingId)
    {
        _pendingMeetingIds.Add(meetingId);
    }

    internal void AcceptInvitation(MeetingId meetingId)
    {
        if (!_pendingMeetingIds.Contains(meetingId))
        {
            return;
        }
        _pendingMeetingIds.Remove(meetingId);
        _upcomingMeetingIds.Add(meetingId);
    }

    public void ChangeName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}