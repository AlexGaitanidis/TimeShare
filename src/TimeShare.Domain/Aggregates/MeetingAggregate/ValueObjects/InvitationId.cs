using TimeShare.Domain.Common.Primitives;

namespace TimeShare.Domain.Aggregates.MeetingAggregate.ValueObjects;

public sealed class InvitationId : StronglyTypedId
{
    public Guid Value { get; private set; }

    private InvitationId(Guid value)
    {
        Value = value;
    }

    private InvitationId() { }

    public static InvitationId CreateUnique()
    {
        return new InvitationId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static InvitationId Create(Guid value)
    {
        return new InvitationId(value);
    }
}