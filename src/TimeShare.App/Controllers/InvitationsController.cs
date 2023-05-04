using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TimeShare.Application.Invitations.Commands.AcceptInvitation;
using TimeShare.Application.Invitations.Commands.SendInvitation;
using TimeShare.Application.Invitations.Queries.GetInvitationById;
using TimeShare.Application.Invitations.Queries.GetInvitationsByMeeting;
using TimeShare.Contracts.Meetings;
using TimeShare.Domain.Aggregates.GuestAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.MeetingAggregate.ValueObjects;

namespace TimeShare.App.Controllers;

[Route("hosts/{hostId:guid}/meetings/{meetingId:guid}/invitations")]
public sealed class InvitationsController : ApiController
{
    public InvitationsController(ISender sender, IMapper mapper) 
        : base(sender, mapper)
    {
    }

    [HttpGet("{invitationId:guid}")]
    public async Task<IActionResult> GetInvitationById(Guid meetingId, Guid invitationId, CancellationToken cancellationToken)
    {
        var query = new GetInvitationByIdQuery(MeetingId.Create(meetingId), InvitationId.Create(invitationId));

        var result = await Sender.Send(query, cancellationToken);

        return result.Match(
            invitation => Ok(Mapper.Map<InvitationResponse>(invitation)),
            Problem);
    }

    [HttpGet]
    public async Task<IActionResult> GetInvitationsByMeeting(Guid meetingId, CancellationToken cancellationToken)
    {
        var query = new GetInvitationsByMeetingQuery(MeetingId.Create(meetingId));

        var result = await Sender.Send(query, cancellationToken);

        return result.Match(
            invitations => Ok(Mapper.Map<List<InvitationResponse>>(invitations)),
            Problem);
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendInvitation(Guid hostId, Guid meetingId, [FromBody] Guid guestId, CancellationToken cancellationToken)
    {
        var command = new SendInvitationCommand(MeetingId.Create(meetingId), GuestId.Create(guestId));

        var result = await Sender.Send(command, cancellationToken);

        return result.Match(
            invitation => CreatedAtAction(
                nameof(GetInvitationById),
                new { hostId, meetingId, invitationId = invitation.Id.Value },
                Mapper.Map<InvitationResponse>(invitation)),
            Problem);
    }

    [HttpPut("{invitationId:guid}/accept")]
    public async Task<IActionResult> AcceptInvitation(Guid meetingId, Guid invitationId, CancellationToken cancellationToken)
    {
        var command = new AcceptInvitationCommand(MeetingId.Create(meetingId), InvitationId.Create(invitationId));

        var result = await Sender.Send(command, cancellationToken);

        return result.Match(
            invitation => Ok(Mapper.Map<InvitationResponse>(invitation)),
            Problem);
    }
}
