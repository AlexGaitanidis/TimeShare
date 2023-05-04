using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Domain.Aggregates.UserAggregate;
using TimeShare.Domain.Aggregates.UserAggregate.ValueObjects;

namespace TimeShare.Application.Users.Commands.UpdateUserName;

public sealed record UpdateUserNameCommand(UserId UserId, string FirstName, string LastName) : ICommand<User>;