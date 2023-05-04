using TimeShare.Application.Abstractions.Services;
using TimeShare.Domain.Aggregates.HostAggregate;
using TimeShare.Domain.Aggregates.UserAggregate;

namespace TimeShare.Infrastructure.Services;

public class EmailService : IEmailService
{
    public async Task SendWelcomeEmailAsync(User user, CancellationToken cancellationToken = default)
    {
        await Task.CompletedTask;
    }

    public async Task SendMeetingCreatedEmailAsync(Host host, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}