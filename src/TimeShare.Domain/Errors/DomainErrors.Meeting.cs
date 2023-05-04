using ErrorOr;
using TimeShare.Domain.Aggregates.GuestAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.MeetingAggregate.ValueObjects;

namespace TimeShare.Domain.Errors;

public static partial class DomainErrors
{
    public static class Meeting
    {
        public static Func<MeetingId, Error> NotFound => id => Error.NotFound(
            code: "Meeting.NotFound",
            description: $"The specified meeting (Id = {id.Value}) was not found.");

        public static Error InvitingHost => Error.Failure(
            code: "Meeting.InvitingHost",
            description: "The meeting host can not be invited.");

        public static Error InvitationForEndedMeeting => Error.Failure(
            code: "Meeting.InvitationForEndedMeeting",
            description: "Invitations can not be sent for an ended meeting.");

        public static Error AlreadyEnded => Error.Failure(
            code: "Meeting.AlreadyEnded",
            description: "This meeting has already ended.");

        public static Error AlreadyCancelled => Error.Failure(
            code: "Meeting.AlreadyCancelled",
            description: "This meeting has already been cancelled.");

        public static Error InvitationForCancelledMeeting => Error.Failure(
            code: "Meeting.InvitationForCancelledMeeting",
            description: "Invitations can not be sent for a cancelled meeting.");

        public static Error InvalidHost => Error.Failure(
            code: "Meeting.InvalidHost",
            description: "Invitations can not be sent from invalid host.");
        
        public static Func<GuestId, Error> AlreadyInvitedGuest => id => Error.Failure(
            code: "Meeting.AlreadyInvitedGuest",
            description: $"The specified guest (Id = {id.Value}) has already been invited.");

        public static Func<int, Error> MaximumGuestsReached => maxGuests => Error.Failure(
            code: "Meeting.MaximumGuestsReached",
            description: $"Invitations can not be sent when the guests have reached the maximum count ({maxGuests}).");
    }
}