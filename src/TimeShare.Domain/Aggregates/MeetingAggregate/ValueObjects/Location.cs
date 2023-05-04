using TimeShare.Domain.Common.Primitives;

namespace TimeShare.Domain.Aggregates.MeetingAggregate.ValueObjects;

public sealed class Location : ValueObject
{
    public string Name { get; private set; }
    public string Address { get; private set; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }

    private Location(string name, string address, double latitude, double longitude)
    {
        Name = name;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
    }

    public static Location Create(string name, string address, double latitude, double longitude)
    {
        return new Location(name, address, latitude, longitude);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Name;
        yield return Address;
        yield return Latitude;
        yield return Longitude;
    }
}