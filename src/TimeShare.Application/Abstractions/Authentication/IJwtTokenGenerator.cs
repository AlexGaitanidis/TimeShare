using TimeShare.Domain.Aggregates.UserAggregate;

namespace TimeShare.Application.Abstractions.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}