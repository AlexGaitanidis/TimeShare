using ErrorOr;
using TimeShare.Domain.Aggregates.MeetingAggregate.ValueObjects;

namespace TimeShare.Domain.Errors;

public static partial class DomainErrors
{
    public static class Invitation
    {
        public static Func<InvitationId, Error> NotFound => id => Error.NotFound(
            code: "Invitation.NotFound",
            description: $"The specified invitation (Id = {id.Value}) was not found.");

        public static Error AlreadyCancelled => Error.Failure(
            code: "Invitation.AlreadyCancelled",
            description: "Invitation has already been cancelled.");

        public static Error Invalid => Error.Failure(
            code: "Invitation.Invalid",
            description: "The invitation is invalid.");

        public static Error Expired => Error.Failure(
            code: "Invitation.Expired",
            description: "The invitation has expired.");
    }
}