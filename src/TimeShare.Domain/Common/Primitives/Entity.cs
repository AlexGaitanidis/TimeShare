namespace TimeShare.Domain.Common.Primitives;

public abstract class Entity<TId> : IEquatable<Entity<TId>>, IAuditableEntity
    where TId : StronglyTypedId
{
    public TId Id { get; }

    public DateTime CreatedOnUtc { get; protected set; }
    public DateTime? ModifiedOnUtc { get; protected set; }

    protected Entity(TId id)
    {
        Id = id;
    }
    
#pragma warning disable CS8618
    protected Entity() { }
#pragma warning restore CS8618

    public bool Equals(Entity<TId>? other)
    {
        return Equals((object?)other);
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && Id.Equals(entity.Id);
    }

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}