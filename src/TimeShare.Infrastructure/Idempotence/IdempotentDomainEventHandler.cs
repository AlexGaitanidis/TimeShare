using MediatR;
using Microsoft.EntityFrameworkCore;
using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Domain.Common.Primitives;
using TimeShare.Persistence;
using TimeShare.Persistence.Outbox;

namespace TimeShare.Infrastructure.Idempotence;

public sealed class IdempotentDomainEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    private readonly INotificationHandler<TDomainEvent> _decorated;
    private readonly TimeShareDbContext _dbContext;

    public IdempotentDomainEventHandler(INotificationHandler<TDomainEvent> decorated, TimeShareDbContext dbContext)
    {
        _decorated = decorated;
        _dbContext = dbContext;
    }

    public async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)
    {
        string consumer = _decorated.GetType().Name;

        bool consumerExists = await _dbContext.OutboxMessageConsumers
            .AnyAsync(
                omc => omc.Id == notification.Id &&
                       omc.Name == consumer,
                cancellationToken);

        if (consumerExists) return;

        await _decorated.Handle(notification, cancellationToken);

        _dbContext.OutboxMessageConsumers
            .Add(new OutboxMessageConsumer
            {
                Id = notification.Id,
                Name = consumer
            });

        await _dbContext.SaveChangesAsync(CancellationToken.None);
    }
}