using TimeShare.Domain.Aggregates.GuestAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.MeetingAggregate.Enums;
using TimeShare.Domain.Aggregates.MeetingAggregate.ValueObjects;
using TimeShare.Domain.Common.Primitives;

namespace TimeShare.Domain.Aggregates.MeetingAggregate.Entities;

public sealed class Invitation : Entity<InvitationId>
{
    public MeetingId MeetingId { get; private set; }
    public GuestId GuestId { get; private set; }
    public InvitationStatus Status { get; private set; } = InvitationStatus.Pending;

    private Invitation(InvitationId id, MeetingId meetingId, GuestId guestId)
        : base(id)
    {
        MeetingId = meetingId;
        GuestId = guestId;
    }
    
    internal static Invitation Create(MeetingId meetingId, GuestId guestId)
    {
        return new Invitation(InvitationId.CreateUnique(), meetingId, guestId);
    }

    internal void Accept()
    {
        Status = InvitationStatus.Accepted;
    }

    internal void Reject()
    {
        Status = InvitationStatus.Rejected;
    }

    internal void Expire()
    {
        Status = InvitationStatus.Expired;
    }

    internal void Cancel()
    {
        Status = InvitationStatus.Cancelled;
    }
}