using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Application.Abstractions.Persistence;
using TimeShare.Domain.Aggregates.HostAggregate;
using TimeShare.Domain.Aggregates.UserAggregate;
using TimeShare.Domain.Aggregates.UserAggregate.Events;

namespace TimeShare.Application.Users.Events.OnUserRegistered;

internal sealed class CreateHostOnUserRegistered : IDomainEventHandler<UserRegistered>
{
    private readonly IUserRepository _userRepository;
    private readonly IHostRepository _hostRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateHostOnUserRegistered(IUserRepository userRepository, IHostRepository hostRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _hostRepository = hostRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UserRegistered notification, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(notification.UserId, cancellationToken);

        if (user is null)
        {
            return;
        }

        var host = Host.Create(user);

        _hostRepository.Add(host);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}