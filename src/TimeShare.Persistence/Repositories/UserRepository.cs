using Microsoft.EntityFrameworkCore;
using TimeShare.Application.Abstractions.Persistence;
using TimeShare.Domain.Aggregates.UserAggregate;
using TimeShare.Domain.Aggregates.UserAggregate.ValueObjects;

namespace TimeShare.Persistence.Repositories;

public class UserRepository : Repository<User, UserId>, IUserRepository
{
    public UserRepository(TimeShareDbContext context) : base(context)
    {
    }

    public async Task<bool> EmailIsUniqueAsync(string email, CancellationToken cancellationToken = default)
    {
        return !await Context.Users.AnyAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await Context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }
}