using TimeShare.Domain.Aggregates.HostAggregate;
using TimeShare.Domain.Aggregates.UserAggregate;

namespace TimeShare.Application.Abstractions.Services;

public interface IEmailService
{
    Task SendWelcomeEmailAsync(User user, CancellationToken cancellationToken = default);
    Task SendMeetingCreatedEmailAsync(Host host, CancellationToken cancellationToken);
}