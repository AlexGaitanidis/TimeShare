using MediatR;
using TimeShare.Domain.Common.Primitives;

namespace TimeShare.Application.Abstractions.Messaging;

public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
    
}