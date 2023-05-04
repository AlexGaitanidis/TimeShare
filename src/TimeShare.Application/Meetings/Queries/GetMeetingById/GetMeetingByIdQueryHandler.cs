using ErrorOr;
using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Application.Abstractions.Persistence;
using TimeShare.Domain.Aggregates.MeetingAggregate;
using TimeShare.Domain.Errors;

namespace TimeShare.Application.Meetings.Queries.GetMeetingById;

internal sealed class GetMeetingByIdQueryHandler : IQueryHandler<GetMeetingByIdQuery, Meeting>
{
    private readonly IMeetingRepository _meetingRepository;

    public GetMeetingByIdQueryHandler(IMeetingRepository meetingRepository)
    {
        _meetingRepository = meetingRepository;
    }

    public async Task<ErrorOr<Meeting>> Handle(GetMeetingByIdQuery request, CancellationToken cancellationToken)
    {
        Meeting? meeting = await _meetingRepository.GetByIdAsync(request.MeetingId, cancellationToken);

        if (meeting is null)
        {
            return DomainErrors.Meeting.NotFound(request.MeetingId);
        }

        return meeting;
    }
}