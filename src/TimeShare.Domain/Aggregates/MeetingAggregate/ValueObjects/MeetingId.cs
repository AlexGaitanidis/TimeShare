using TimeShare.Domain.Common.Primitives;

namespace TimeShare.Domain.Aggregates.MeetingAggregate.ValueObjects;

public sealed class MeetingId : StronglyTypedId
{
    public Guid Value { get; private set; }

    private MeetingId(Guid value)
    {
        Value = value;
    }

    private MeetingId() { }

    public static MeetingId CreateUnique()
    {
        return new MeetingId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static MeetingId Create(Guid value)
    {
        return new MeetingId(value);
    }
}