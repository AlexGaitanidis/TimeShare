using ErrorOr;
using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Application.Abstractions.Persistence;
using TimeShare.Domain.Aggregates.MeetingAggregate;
using TimeShare.Domain.Aggregates.MeetingAggregate.Entities;
using TimeShare.Domain.Errors;

namespace TimeShare.Application.Invitations.Queries.GetInvitationsByMeeting;

internal sealed class GetInvitationsByMeetingQueryHandler : IQueryHandler<GetInvitationsByMeetingQuery, IEnumerable<Invitation>>
{
    private readonly IMeetingRepository _meetingRepository;

    public GetInvitationsByMeetingQueryHandler(IMeetingRepository meetingRepository)
    {
        _meetingRepository = meetingRepository;
    }

    public async Task<ErrorOr<IEnumerable<Invitation>>> Handle(GetInvitationsByMeetingQuery request, CancellationToken cancellationToken)
    {
        Meeting? meeting = await _meetingRepository.GetByIdAsync(request.MeetingId, cancellationToken);

        if (meeting is null)
        {
            return DomainErrors.Meeting.NotFound(request.MeetingId);
        }

        return meeting.Invitations.ToList();
    }
}