using Microsoft.EntityFrameworkCore;
using TimeShare.Application.Abstractions.Persistence;
using TimeShare.Domain.Aggregates.GuestAggregate;
using TimeShare.Domain.Aggregates.GuestAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.UserAggregate.ValueObjects;

namespace TimeShare.Persistence.Repositories;

public class GuestRepository : Repository<Guest, GuestId>, IGuestRepository
{
    public GuestRepository(TimeShareDbContext context) : base(context)
    {
    }

    public async Task<Guest?> GetByUserIdAsync(UserId userId, CancellationToken cancellationToken)
    {
        return await Context.Guests.FirstOrDefaultAsync(g => g.UserId == userId, cancellationToken);
    }
}