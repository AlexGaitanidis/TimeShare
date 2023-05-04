using TimeShare.Domain.Aggregates.UserAggregate.Events;
using TimeShare.Domain.Aggregates.UserAggregate.ValueObjects;
using TimeShare.Domain.Common.Primitives;

namespace TimeShare.Domain.Aggregates.UserAggregate;

public sealed class User : AggregateRoot<UserId>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    private User(UserId id, string firstName, string lastName, string email, string password)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
    }

    public static User Create(string firstName, string lastName, string email, string password)
    {
        var user = new User(UserId.CreateUnique(), firstName, lastName, email, password);

        user.RaiseDomainEvent(new UserRegistered(Guid.NewGuid(), user.Id));

        return user;
    }

    public void ChangeName(string firstName, string lastName)
    {
        if (FirstName != firstName || LastName != lastName )
        {
            RaiseDomainEvent(new UserNameChanged(Guid.NewGuid(), Id));
        }

        FirstName = firstName; 
        LastName = lastName;
    }
}