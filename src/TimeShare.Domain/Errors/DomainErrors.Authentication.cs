using ErrorOr;

namespace TimeShare.Domain.Errors;

public static partial class DomainErrors
{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Validation(
            code: "Auth.InvalidCredentials",
            description: "The provided credentials are invalid");
    }
}