using ErrorOr;
using TimeShare.Domain.Aggregates.UserAggregate.ValueObjects;

namespace TimeShare.Domain.Errors;

public static partial class DomainErrors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(
            code: "User.DuplicateEmail",
            description: "The email is already in use.");

        public static Func<UserId, Error> NotFound => id => Error.NotFound(
            code: "User.NotFound",
            description: $"The specified user (Id = {id.Value}) was not found.");
    }
}