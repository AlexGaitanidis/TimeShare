using ErrorOr;
using TimeShare.Domain.Aggregates.HostAggregate.ValueObjects;

namespace TimeShare.Domain.Errors;

public static partial class DomainErrors
{
    public static class Host
    {
        public static Func<HostId, Error> NotFound => id => Error.NotFound(
            code: "Host.NotFound",
            description: $"The specified host (Id = {id.Value}) was not found.");
    }
}