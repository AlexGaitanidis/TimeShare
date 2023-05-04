using ErrorOr;
using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Application.Abstractions.Persistence;
using TimeShare.Domain.Aggregates.UserAggregate;

namespace TimeShare.Application.Users.Queries.GetUsers;

internal sealed class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, IEnumerable<User>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<IEnumerable<User>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetAllAsync(cancellationToken);
    }
}