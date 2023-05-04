using TimeShare.Domain.Aggregates.UserAggregate;

namespace TimeShare.Application.Authentication.Common;

public record AuthenticationResult(User User, string Token);