using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Domain.Aggregates.UserAggregate;

namespace TimeShare.Application.Users.Queries.GetUsers;

public sealed record GetUsersQuery() : IQuery<IEnumerable<User>>;