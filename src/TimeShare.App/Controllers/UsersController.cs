using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TimeShare.Application.Users.Commands.UpdateUserName;
using TimeShare.Application.Users.Queries.GetUserById;
using TimeShare.Application.Users.Queries.GetUsers;
using TimeShare.Contracts.Users;
using TimeShare.Domain.Aggregates.UserAggregate.ValueObjects;

namespace TimeShare.App.Controllers;

[Route("users")]
public sealed class UsersController : ApiController
{
    public UsersController(ISender sender, IMapper mapper) 
        : base(sender, mapper)
    {
    }

    [HttpPut("{userId:guid}")]
    public async Task<IActionResult> UpdateUserName(Guid userId, UpdateUserNameRequest request, CancellationToken cancellationToken)
    {
        var command = Mapper.Map<UpdateUserNameCommand>((userId, request));

        var userResult = await Sender.Send(command, cancellationToken);

        return userResult.Match(
            user => Ok(Mapper.Map<UserResponse>(user)),
            Problem);
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserById(Guid userId, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(UserId.Create(userId));

        var userResult = await Sender.Send(query, cancellationToken);

        return userResult.Match(
            user => Ok(Mapper.Map<UserResponse>(user)),
            Problem);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
    {
        var query = new GetUsersQuery();

        var usersResult = await Sender.Send(query, cancellationToken);

        return usersResult.Match(
            users => Ok(Mapper.Map<IEnumerable<UserResponse>>(users)),
            Problem);
    }
}