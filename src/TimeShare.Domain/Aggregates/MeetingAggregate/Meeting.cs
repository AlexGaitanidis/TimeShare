using ErrorOr;
using TimeShare.Domain.Aggregates.GuestAggregate;
using TimeShare.Domain.Aggregates.HostAggregate;
using TimeShare.Domain.Aggregates.HostAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.MeetingAggregate.Entities;
using TimeShare.Domain.Aggregates.MeetingAggregate.Enums;
using TimeShare.Domain.Aggregates.MeetingAggregate.Events;
using TimeShare.Domain.Aggregates.MeetingAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.MeetingReviewAggregate.ValueObjects;
using TimeShare.Domain.Common.Primitives;
using TimeShare.Domain.Common.ValueObjects;
using TimeShare.Domain.Errors;

namespace TimeShare.Domain.Aggregates.MeetingAggregate;

public sealed class Meeting : AggregateRoot<MeetingId>
{
    private readonly List<Invitation> _invitations = new();
    private readonly List<MeetingReviewId> _meetingReviewIds = new();

    public string Name { get; private set; }
    public string Description { get; private set; }
    public AverageRating AverageRating { get; private set; }
    public DateTime StartOnUtc { get; private set; }
    public DateTime EndOnUtc { get; private set; }
    public int? MaxGuests { get; private set; }
    public int GuestCount { get; private set; }
    public HostId HostId { get; private set; }
    public Location? Location { get; private set; }
    public MeetingStatus Status { get; private set; } = MeetingStatus.Upcoming;

    public IReadOnlyList<Invitation> Invitations => _invitations.AsReadOnly();
    public IReadOnlyList<MeetingReviewId> MeetingReviewIds => _meetingReviewIds.AsReadOnly();

#pragma warning disable CS8618
    private Meeting() { }
#pragma warning restore CS8618

    private Meeting(
        MeetingId id,
        string name,
        string description,
        AverageRating averageRating,
        DateTime startOnUtc,
        DateTime endOnUtc,
        int? maxGuests,
        HostId hostId,
        Location? location) : base(id)
    {
        Name = name;
        Description = description;
        AverageRating = averageRating;
        StartOnUtc = startOnUtc;
        EndOnUtc = endOnUtc;
        MaxGuests = maxGuests;
        HostId = hostId;
        Location = location;
    }
    
    public static Meeting Create(
        string name,
        string description,
        DateTime startOnUtc,
        DateTime endOnUtc,
        int? maxGuests,
        HostId hostId,
        Location? location)
    {
        var meeting = new Meeting(
            MeetingId.CreateUnique(),
            name,
            description,
            AverageRating.CreateNew(), 
            startOnUtc,
            endOnUtc,
            maxGuests,
            hostId,
            location);

        meeting.RaiseDomainEvent(new MeetingCreated(Guid.NewGuid(), meeting.Id, meeting.HostId));

        return meeting;
    }

    public ErrorOr<Success> Cancel()
    {
        switch (Status)
        {
            case MeetingStatus.Ended:
                return DomainErrors.Meeting.AlreadyEnded;
            case MeetingStatus.Cancelled:
                return DomainErrors.Meeting.AlreadyCancelled;
        }

        Status = MeetingStatus.Cancelled;

        RaiseDomainEvent(new MeetingCancelled(Guid.NewGuid(), Id));

        return Result.Success;
    }

    public ErrorOr<Invitation> SendInvitation(Host host, Guest guest)
    {
        switch (Status)
        {
            case MeetingStatus.Ended:
                return DomainErrors.Meeting.InvitationForEndedMeeting;
            case MeetingStatus.Cancelled:
                return DomainErrors.Meeting.InvitationForCancelledMeeting;
        }

        if (MaxGuests == GuestCount)
        {
            return DomainErrors.Meeting.MaximumGuestsReached(GuestCount);
        }

        if (host.Id != HostId)
        {
            return DomainErrors.Meeting.InvalidHost;
        }

        if (host.UserId == guest.UserId)
        {
            return DomainErrors.Meeting.InvitingHost;
        }
        
        if (_invitations.Any(i => i.GuestId == guest.Id))
        {
            return DomainErrors.Meeting.AlreadyInvitedGuest(guest.Id);
        }

        var invitation = Invitation.Create(Id, guest.Id);

        _invitations.Add(invitation);

        RaiseDomainEvent(new InvitationSent(Guid.NewGuid(), invitation.Id));

        return invitation;
    }

    public ErrorOr<Invitation> AcceptInvitation(InvitationId invitationId)
    {
        var invitation = _invitations.FirstOrDefault(i => i.Id == invitationId);

        if (invitation is null)
        {
            return DomainErrors.Invitation.NotFound(invitationId);
        }

        if (invitation.Status is not InvitationStatus.Pending or InvitationStatus.Rejected)
        {
            return DomainErrors.Invitation.Invalid;
        }

        if (MaxGuests == GuestCount)
        {
            return DomainErrors.Meeting.MaximumGuestsReached(GuestCount);
        }

        invitation.Accept();

        GuestCount++;

        RaiseDomainEvent(new InvitationAccepted(Guid.NewGuid(), invitationId, Id));

        return invitation;
    }

    public ErrorOr<Invitation> CancelInvitation(Invitation invitation)
    {
        if (!Invitations.Contains(invitation))
        {
            return DomainErrors.Invitation.Invalid;
        }

        if (invitation.Status == InvitationStatus.Cancelled)
        {
            return DomainErrors.Invitation.AlreadyCancelled;
        }

        invitation.Cancel();

        if (invitation.Status == InvitationStatus.Accepted)
        {
            GuestCount--;
        }

        return invitation;
    }
}