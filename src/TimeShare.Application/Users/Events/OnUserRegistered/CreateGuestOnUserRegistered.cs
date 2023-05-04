using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Application.Abstractions.Persistence;
using TimeShare.Domain.Aggregates.GuestAggregate;
using TimeShare.Domain.Aggregates.UserAggregate;
using TimeShare.Domain.Aggregates.UserAggregate.Events;

namespace TimeShare.Application.Users.Events.OnUserRegistered;

internal sealed class CreateGuestOnUserRegistered : IDomainEventHandler<UserRegistered>
{
    private readonly IUserRepository _userRepository;
    private readonly IGuestRepository _guestRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateGuestOnUserRegistered(IUserRepository userRepository, IGuestRepository guestRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _guestRepository = guestRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UserRegistered notification, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(notification.UserId, cancellationToken);

        if (user is null)
        {
            return;
        }

        var guest = Guest.Create(user);

        _guestRepository.Add(guest);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}