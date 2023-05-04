using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Application.Abstractions.Persistence;
using TimeShare.Domain.Aggregates.UserAggregate;
using TimeShare.Domain.Aggregates.HostAggregate;
using TimeShare.Domain.Aggregates.UserAggregate.Events;

namespace TimeShare.Application.Users.Events.OnUserNameChanged;

public class ChangeHostNameOnUserNameChanged : IDomainEventHandler<UserNameChanged>
{
    private readonly IUserRepository _userRepository;
    private readonly IHostRepository _hostRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeHostNameOnUserNameChanged(IUserRepository userRepository, IHostRepository hostRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _hostRepository = hostRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UserNameChanged notification, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(notification.UserId, cancellationToken);

        if (user is null) return;

        Host? host = await _hostRepository.GetByUserIdAsync(notification.UserId, cancellationToken);

        if (host is null) return;

        host.ChangeName(user.FirstName, user.LastName);

        _hostRepository.Update(host);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}