using MediatR;
using TimeShare.Application.Abstractions.Persistence;
using TimeShare.Domain.Aggregates.GuestAggregate;
using TimeShare.Domain.Aggregates.UserAggregate;
using TimeShare.Domain.Aggregates.UserAggregate.Events;

namespace TimeShare.Application.Users.Events.OnUserNameChanged;

internal sealed class ChangeGuestNameOnUserNameChanged : INotificationHandler<UserNameChanged>
{
    private readonly IUserRepository _userRepository;
    private readonly IGuestRepository _guestRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeGuestNameOnUserNameChanged(IUserRepository userRepository, IGuestRepository guestRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _guestRepository = guestRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UserNameChanged notification, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(notification.UserId, cancellationToken);

        if (user is null) return;

        Guest? guest = await _guestRepository.GetByUserIdAsync(notification.UserId, cancellationToken);

        if (guest is null) return;

        guest.ChangeName(user.FirstName, user.LastName);

        _guestRepository.Update(guest);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}