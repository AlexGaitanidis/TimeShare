using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Application.Abstractions.Persistence;
using TimeShare.Application.Abstractions.Services;
using TimeShare.Domain.Aggregates.HostAggregate;
using TimeShare.Domain.Aggregates.MeetingAggregate.Events;

namespace TimeShare.Application.Meetings.Events;

internal sealed class MeetingCreatedHandler : IDomainEventHandler<MeetingCreated>
{
    private readonly IEmailService _emailService;
    private readonly IHostRepository _hostRepository;

    public MeetingCreatedHandler(IEmailService emailService, IHostRepository hostRepository)
    {
        _emailService = emailService;
        _hostRepository = hostRepository;
    }

    public async Task Handle(MeetingCreated notification, CancellationToken cancellationToken)
    {
        Host? host = await _hostRepository.GetByIdAsync(notification.HostId, cancellationToken);

        if (host is null)
        {
            return;
        }

        await _emailService.SendMeetingCreatedEmailAsync(host, cancellationToken);
    }
}