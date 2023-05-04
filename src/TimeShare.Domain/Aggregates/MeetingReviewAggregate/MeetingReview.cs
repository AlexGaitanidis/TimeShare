using TimeShare.Domain.Aggregates.GuestAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.HostAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.MeetingAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.MeetingReviewAggregate.ValueObjects;
using TimeShare.Domain.Common.Primitives;
using TimeShare.Domain.Common.ValueObjects;

namespace TimeShare.Domain.Aggregates.MeetingReviewAggregate;

public sealed class MeetingReview : AggregateRoot<MeetingReviewId>
{
    public Rating Rating { get; private set; }
    public string Comment { get; private set; }
    public MeetingId MeetingId { get; private set; }
    public HostId HostId { get; private set; }
    public GuestId GuestId { get; private set; }

    private MeetingReview(MeetingReviewId id, Rating rating, string comment, MeetingId meetingId, HostId hostId, GuestId guestId)
        : base(id)
    {
        Rating = rating;
        Comment = comment;
        MeetingId = meetingId;
        HostId = hostId;
        GuestId = guestId;
    }

    public static MeetingReview Create(Rating rating, string comment, MeetingId meetingId, HostId hostId, GuestId guestId)
    {
        return new MeetingReview(MeetingReviewId.CreateUnique(), rating, comment, meetingId, hostId, guestId);
    }
}