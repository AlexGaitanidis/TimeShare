using ErrorOr;
using TimeShare.Domain.Aggregates.GuestAggregate.ValueObjects;

namespace TimeShare.Domain.Errors;

public static partial class DomainErrors
{
    public static class Guest
    {
        public static Func<GuestId, Error> NotFound => id => Error.NotFound(
            code: "Guest.NotFound",
            description: $"The specified guest (Id = {id.Value}) was not found.");
    }
}