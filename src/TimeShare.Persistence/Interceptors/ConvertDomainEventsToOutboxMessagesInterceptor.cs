using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using TimeShare.Domain.Common.Primitives;
using TimeShare.Persistence.Outbox;

namespace TimeShare.Persistence.Interceptors;

public sealed class ConvertDomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
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

        var outboxMessages = dbContext.ChangeTracker
            .Entries<IAggregateRoot>()
            .Select(x => x.Entity)
            .SelectMany(ar =>
            {
                List<IDomainEvent> domainEvents = ar.GetDomainEvents().ToList();

                ar.ClearDomainEvents();

                return domainEvents;
            })
            .Select(de => new OutboxMessage
            {
                Id = de.Id,
                Type = de.GetType().Name,
                Content = JsonConvert.SerializeObject(de,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    }),
                OccurredOnUtc = DateTime.UtcNow
            })
            .ToList();

        dbContext.Set<OutboxMessage>().AddRange(outboxMessages);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}