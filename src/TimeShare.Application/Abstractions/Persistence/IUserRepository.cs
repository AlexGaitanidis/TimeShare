using TimeShare.Domain.Aggregates.UserAggregate;
using TimeShare.Domain.Aggregates.UserAggregate.ValueObjects;

namespace TimeShare.Application.Abstractions.Persistence;

public interface IUserRepository : IRepository<User, UserId>
{
    Task<bool> EmailIsUniqueAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}