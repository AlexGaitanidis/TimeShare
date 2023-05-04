using ErrorOr;
using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Application.Abstractions.Persistence;
using TimeShare.Domain.Aggregates.GuestAggregate;
using TimeShare.Domain.Aggregates.HostAggregate;
using TimeShare.Domain.Aggregates.MeetingAggregate;
using TimeShare.Domain.Aggregates.MeetingAggregate.Entities;
using TimeShare.Domain.Errors;

namespace TimeShare.Application.Invitations.Commands.SendInvitation;

internal sealed class SendInvitationCommandHandler : ICommandHandler<SendInvitationCommand, Invitation>
{
    private readonly IMeetingRepository _meetingRepository;
    private readonly IHostRepository _hostRepository;
    private readonly IGuestRepository _guestRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SendInvitationCommandHandler(IMeetingRepository meetingRepository, IHostRepository hostRepository, IGuestRepository guestRepository, IUnitOfWork unitOfWork)
    {
        _meetingRepository = meetingRepository;
        _hostRepository = hostRepository;
        _guestRepository = guestRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Invitation>> Handle(SendInvitationCommand request, CancellationToken cancellationToken)
    {
        Meeting? meeting = await _meetingRepository.GetByIdAsync(request.MeetingId, cancellationToken);

        if (meeting is null)
        {
            return DomainErrors.Meeting.NotFound(request.MeetingId);
        }

        Host? host = await _hostRepository.GetByIdAsync(meeting.HostId, cancellationToken);

        if (host is null)
        {
            return DomainErrors.Host.NotFound(meeting.HostId);
        }

        Guest? guest = await _guestRepository.GetByIdAsync(request.GuestId, cancellationToken);

        if (guest is null)
        {
            return DomainErrors.Guest.NotFound(request.GuestId);
        }

        var invitation = meeting.SendInvitation(host, guest);

        if (invitation.IsError)
        {
            return invitation;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return invitation;
    }
}