using ErrorOr;
using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Application.Abstractions.Persistence;
using TimeShare.Domain.Aggregates.MeetingAggregate.Entities;
using TimeShare.Domain.Errors;

namespace TimeShare.Application.Invitations.Commands.AcceptInvitation;

internal sealed class AcceptInvitationCommandHandler : ICommandHandler<AcceptInvitationCommand, Invitation>
{
    private readonly IMeetingRepository _meetingRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AcceptInvitationCommandHandler(IMeetingRepository meetingRepository, IUnitOfWork unitOfWork)
    {
        _meetingRepository = meetingRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Invitation>> Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
    {
        var meeting = await _meetingRepository.GetByIdAsync(request.MeetingId, cancellationToken);

        if (meeting is null)
        {
            return DomainErrors.Meeting.NotFound(request.MeetingId);
        }

        var invitation = meeting.AcceptInvitation(request.InvitationId);

        if (invitation.IsError)
        {
            return invitation;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return invitation;
    }
}