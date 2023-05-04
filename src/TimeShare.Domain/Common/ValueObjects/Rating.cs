using TimeShare.Domain.Common.Primitives;

namespace TimeShare.Domain.Common.ValueObjects;

public sealed class Rating : ValueObject
{
    public double Value { get; private set; }

    private Rating(double value)
    {
        Value = value;
    }

    public static Rating Create(double value)
    {
        return new Rating(value);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}