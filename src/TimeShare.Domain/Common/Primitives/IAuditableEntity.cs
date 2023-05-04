namespace TimeShare.Domain.Common.Primitives;

public interface IAuditableEntity
{
    DateTime CreatedOnUtc { get; }
    DateTime? ModifiedOnUtc { get; }
}