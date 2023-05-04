using ErrorOr;
using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Application.Abstractions.Persistence;
using TimeShare.Domain.Aggregates.MeetingAggregate;
using TimeShare.Domain.Aggregates.MeetingAggregate.Entities;
using TimeShare.Domain.Errors;

namespace TimeShare.Application.Invitations.Queries.GetInvitationById;

internal sealed class GetInvitationByIdQueryHandler : IQueryHandler<GetInvitationByIdQuery, Invitation>
{
    private readonly IMeetingRepository _meetingRepository;

    public GetInvitationByIdQueryHandler(IMeetingRepository meetingRepository)
    {
        _meetingRepository = meetingRepository;
    }

    public async Task<ErrorOr<Invitation>> Handle(GetInvitationByIdQuery request, CancellationToken cancellationToken)
    {
        Meeting? meeting = await _meetingRepository.GetByIdAsync(request.MeetingId, cancellationToken);

        if (meeting is null)
        {
            return DomainErrors.Meeting.NotFound(request.MeetingId);
        }

        Invitation? invitation = meeting.Invitations.FirstOrDefault(i => i.Id == request.InvitationId);

        if (invitation is null)
        {
            return DomainErrors.Invitation.NotFound(request.InvitationId);
        }

        return invitation;
    }
}