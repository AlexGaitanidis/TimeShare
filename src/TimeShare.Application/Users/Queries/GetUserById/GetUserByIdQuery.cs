using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Domain.Aggregates.UserAggregate;
using TimeShare.Domain.Aggregates.UserAggregate.ValueObjects;

namespace TimeShare.Application.Users.Queries.GetUserById;

public sealed record GetUserByIdQuery(UserId UserId) : IQuery<User>;