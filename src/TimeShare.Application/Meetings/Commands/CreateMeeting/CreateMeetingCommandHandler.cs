using ErrorOr;
using TimeShare.Application.Abstractions.Messaging;
using TimeShare.Application.Abstractions.Persistence;
using TimeShare.Domain.Aggregates.MeetingAggregate;
using TimeShare.Domain.Aggregates.MeetingAggregate.ValueObjects;
using TimeShare.Domain.Errors;

namespace TimeShare.Application.Meetings.Commands.CreateMeeting;

internal sealed class CreateMeetingCommandHandler : ICommandHandler<CreateMeetingCommand, Meeting>
{
    private readonly IHostRepository _hostRepository;
    private readonly IMeetingRepository _meetingRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMeetingCommandHandler(IHostRepository hostRepository, IMeetingRepository meetingRepository, IUnitOfWork unitOfWork)
    {
        _hostRepository = hostRepository;
        _meetingRepository = meetingRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Meeting>> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
    {
        bool hostExists = await _hostRepository.HostExists(request.HostId, cancellationToken);

        if (!hostExists)
        {
            return DomainErrors.Host.NotFound(request.HostId);
        }

        Location? location = request.Location is null 
            ? null 
            : Location.Create(request.Location.Name, request.Location.Address, request.Location.Latitude, request.Location.Longitude);
        
        Meeting meeting = Meeting.Create(
            request.Name,
            request.Description,
            request.StartOnUtc,
            request.EndOnUtc,
            request.MaxGuests,
            request.HostId,
            location);

        _meetingRepository.Add(meeting);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return meeting;
    }
}