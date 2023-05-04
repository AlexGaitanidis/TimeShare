using ErrorOr;
using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Application.Abstractions.Persistence;
using TimeShare.Domain.Aggregates.UserAggregate;
using TimeShare.Domain.Errors;

namespace TimeShare.Application.Users.Commands.UpdateUserName;

internal sealed class UpdateUserNameCommandHandler : ICommandHandler<UpdateUserNameCommand, User>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserNameCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<User>> Handle(UpdateUserNameCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return DomainErrors.User.NotFound(request.UserId);
        }

        user.ChangeName(request.FirstName, request.LastName);

        _userRepository.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user;
    }
}