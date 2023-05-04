using ErrorOr;
using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Application.Abstractions.Persistence;
using TimeShare.Domain.Aggregates.MeetingAggregate;

namespace TimeShare.Application.Meetings.Queries.GetMeetings;

internal sealed class GetMeetingsQueryHandler : IQueryHandler<GetMeetingsQuery, IEnumerable<Meeting>>
{
    private readonly IMeetingRepository _meetingRepository;

    public GetMeetingsQueryHandler(IMeetingRepository meetingRepository)
    {
        _meetingRepository = meetingRepository;
    }

    public async Task<ErrorOr<IEnumerable<Meeting>>> Handle(GetMeetingsQuery request, CancellationToken cancellationToken)
    {
        List<Meeting> meetings = await _meetingRepository.GetAllAsync(cancellationToken);
        
        return meetings;
    }
}