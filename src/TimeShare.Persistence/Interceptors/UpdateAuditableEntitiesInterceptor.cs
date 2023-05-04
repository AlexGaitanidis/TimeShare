using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TimeShare.Domain.Common.Primitives;

namespace TimeShare.Persistence.Interceptors;

public sealed class UpdateAuditableEntitiesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        DbContext? dbContext = eventData.Context;

        if (dbContext is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        IEnumerable<EntityEntry<IAuditableEntity>> entities = dbContext.ChangeTracker.Entries<IAuditableEntity>();

        foreach (var entity in entities)
        {
            if (entity.State == EntityState.Added)
            {
                entity.Property(e => e.CreatedOnUtc).CurrentValue = DateTime.UtcNow;
            }

            if (entity.State == EntityState.Modified)
            {
                entity.Property(e => e.ModifiedOnUtc).CurrentValue = DateTime.UtcNow;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}