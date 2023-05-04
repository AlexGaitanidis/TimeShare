using MediatR;

namespace TimeShare.Domain.Common.Primitives;

public interface IDomainEvent : INotification
{
    public Guid Id { get; init; }
}