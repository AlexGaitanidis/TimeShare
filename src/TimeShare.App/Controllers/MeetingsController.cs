using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TimeShare.Application.Meetings.Commands.CreateMeeting;
using TimeShare.Application.Meetings.Queries.GetMeetingById;
using TimeShare.Application.Meetings.Queries.GetMeetings;
using TimeShare.Application.Meetings.Queries.GetMeetingsByHost;
using TimeShare.Contracts.Meetings;
using TimeShare.Domain.Aggregates.HostAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.MeetingAggregate.ValueObjects;

namespace TimeShare.App.Controllers;

[Route("hosts/{hostId:guid}/meetings")]
public sealed class MeetingsController : ApiController
{
    public MeetingsController(ISender sender, IMapper mapper) 
        : base(sender, mapper)
    {
    }

    [HttpGet("{meetingId:guid}")]
    public async Task<IActionResult> GetMeetingById(Guid meetingId, CancellationToken cancellationToken)
    {
        var query = new GetMeetingByIdQuery(MeetingId.Create(meetingId));

        var meetingResult = await Sender.Send(query, cancellationToken);

        return meetingResult.Match(
            meeting => Ok(Mapper.Map<MeetingResponse>(meeting)),
            Problem);
    }

    [HttpGet("/meetings")]
    public async Task<IActionResult> GetMeetings(CancellationToken cancellationToken)
    {
        var query = new GetMeetingsQuery();

        var meetingsResult = await Sender.Send(query, cancellationToken);

        return meetingsResult.Match(
            meetings => Ok(Mapper.Map<IEnumerable<MeetingResponse>>(meetings)),
            Problem);
    }

    [HttpGet]
    public async Task<IActionResult> GetMeetingsByHost(Guid hostId, CancellationToken cancellationToken)
    {
        var query = new GetMeetingsByHostQuery(HostId.Create(hostId));

        var meetingsResult = await Sender.Send(query, cancellationToken);

        return meetingsResult.Match(
            meetings => Ok(Mapper.Map<IEnumerable<MeetingResponse>>(meetings)),
            Problem);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMeeting(CreateMeetingRequest request, Guid hostId, CancellationToken cancellationToken)
    {
        var command = Mapper.Map<CreateMeetingCommand>((request, hostId));

        var meetingResult = await Sender.Send(command, cancellationToken);

        return meetingResult.Match(
            meeting => CreatedAtAction(
                nameof(GetMeetingById),
                new { hostId, meetingId = meeting.Id.Value },
                Mapper.Map<MeetingResponse>(meeting)),
            Problem);
    }
}