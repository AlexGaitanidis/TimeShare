using TimeShare.Domain.Common.Primitives;

namespace TimeShare.Domain.Aggregates.GuestAggregate.ValueObjects;

public sealed class GuestId : StronglyTypedId
{
    public Guid Value { get; private set; }

    private GuestId(Guid value)
    {
        Value = value;
    }

    private GuestId() { }

    public static GuestId CreateUnique()
    {
        return new GuestId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static GuestId Create(Guid value)
    {
        return new GuestId(value);
    }
}