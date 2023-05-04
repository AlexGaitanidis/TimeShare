using TimeShare.Domain.Aggregates.HostAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.MeetingAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.UserAggregate;
using TimeShare.Domain.Aggregates.UserAggregate.ValueObjects;
using TimeShare.Domain.Common.Primitives;
using TimeShare.Domain.Common.ValueObjects;

namespace TimeShare.Domain.Aggregates.HostAggregate;

public sealed class Host : AggregateRoot<HostId>
{
    private readonly List<MeetingId> _meetingIds = new();

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public AverageRating AverageRating { get; private set; }
    public UserId UserId { get; private set; }

    public IReadOnlyList<MeetingId> MeetingIds => _meetingIds.AsReadOnly();

    private Host(HostId id, string firstName, string lastName, AverageRating averageRating, UserId userId) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        AverageRating = averageRating;
        UserId = userId;
    }

#pragma warning disable CS8618
    private Host() { }
#pragma warning restore CS8618

    public static Host Create(User user)
    {
        return new Host(HostId.CreateUnique(), user.FirstName, user.LastName, AverageRating.CreateNew(), user.Id);
    }

    public void ChangeName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}