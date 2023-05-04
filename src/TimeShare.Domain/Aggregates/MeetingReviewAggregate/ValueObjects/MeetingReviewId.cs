using TimeShare.Domain.Common.Primitives;

namespace TimeShare.Domain.Aggregates.MeetingReviewAggregate.ValueObjects;

public sealed class MeetingReviewId : StronglyTypedId
{
    public Guid Value { get; private set; }

    private MeetingReviewId(Guid value)
    {
        Value = value;
    }

    private MeetingReviewId() { }

    public static MeetingReviewId CreateUnique()
    {
        return new MeetingReviewId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static MeetingReviewId Create(Guid value)
    {
        return new MeetingReviewId(value);
    }
}