using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Application.Abstractions.Persistence;
using TimeShare.Application.Abstractions.Services;
using TimeShare.Domain.Aggregates.UserAggregate;
using TimeShare.Domain.Aggregates.UserAggregate.Events;

namespace TimeShare.Application.Users.Events.OnUserRegistered;

internal sealed class SendWelcomeEmailOnUserRegistered : IDomainEventHandler<UserRegistered>
{
    private readonly IEmailService _emailService;
    private readonly IUserRepository _userRepository;

    public SendWelcomeEmailOnUserRegistered(IEmailService emailService, IUserRepository userRepository)
    {
        _emailService = emailService;
        _userRepository = userRepository;
    }

    public async Task Handle(UserRegistered notification, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(notification.UserId, cancellationToken);

        if (user is null)
        {
            return;
        }

        await _emailService.SendWelcomeEmailAsync(user, cancellationToken);
    }
}