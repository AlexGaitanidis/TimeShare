using TimeShare.Domain.Common.Primitives;

namespace TimeShare.Domain.Common.ValueObjects;

public sealed class AverageRating : ValueObject
{
    public double? Value { get; private set; }
    public int RatingsCount { get; private set; }

    private AverageRating(double? value, int ratingsCount)
    {
        Value = value;
        RatingsCount = ratingsCount;
    }

    public static AverageRating CreateNew() // (double? rating = null, int ratingsCount = 0)
    {
        return new AverageRating(null, 0);
    }

    public void AddNewRating(Rating rating)
    {
        if (RatingsCount == 0)
        {
            Value = 0;
        }

        Value = (Value * RatingsCount + rating.Value) / ++RatingsCount;
    }

    internal void RemoveRating(Rating rating)
    {
        Value = (Value * RatingsCount - rating.Value) / --RatingsCount;
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
        yield return RatingsCount;
    }
}