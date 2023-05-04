using ErrorOr;
using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Application.Abstractions.Persistence;
using TimeShare.Domain.Aggregates.MeetingAggregate;
using TimeShare.Domain.Errors;

namespace TimeShare.Application.Meetings.Queries.GetMeetingsByHost;

internal sealed class GetMeetingsByHostQueryHandler : IQueryHandler<GetMeetingsByHostQuery, IEnumerable<Meeting>>
{
    private readonly IMeetingRepository _meetingRepository;
    private readonly IHostRepository _hostRepository;

    public GetMeetingsByHostQueryHandler(IMeetingRepository meetingRepository, IHostRepository hostRepository)
    {
        _meetingRepository = meetingRepository;
        _hostRepository = hostRepository;
    }

    public async Task<ErrorOr<IEnumerable<Meeting>>> Handle(GetMeetingsByHostQuery request, CancellationToken cancellationToken)
    {
        bool hostExists = await _hostRepository.HostExists(request.HostId, cancellationToken);

        if (!hostExists)
        {
            return DomainErrors.Host.NotFound(request.HostId);
        }

        List<Meeting> meetings = await _meetingRepository.GetByHostAsync(request.HostId, cancellationToken);

        return meetings;
    }
}