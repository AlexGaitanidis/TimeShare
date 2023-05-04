using TimeShare.Domain.Common.Primitives;

namespace TimeShare.Domain.Aggregates.HostAggregate.ValueObjects;

public sealed class HostId : StronglyTypedId
{
    public Guid Value { get; private set; }

    private HostId(Guid value)
    {
        Value = value;
    }

    private HostId() { }

    public static HostId CreateUnique()
    {
        return new HostId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static HostId Create(Guid value)
    {
        return new HostId(value);
    }
}