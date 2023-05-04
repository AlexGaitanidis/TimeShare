using TimeShare.Domain.Common.Primitives;

namespace TimeShare.Domain.Aggregates.UserAggregate.ValueObjects;

public sealed class UserId : StronglyTypedId
{
    public Guid Value { get; private set; }
    
    private UserId(Guid value)
    {
        Value = value;
    }

    private UserId() { }

    public static UserId CreateUnique()
    {
        return new UserId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static UserId Create(Guid value)
    {
        return new UserId(value);
    }
}