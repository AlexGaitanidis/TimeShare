using Mapster;
using TimeShare.Application.Meetings.Commands.CreateMeeting;
using TimeShare.Contracts.Meetings;
using TimeShare.Domain.Aggregates.HostAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.MeetingAggregate;
using TimeShare.Domain.Aggregates.MeetingAggregate.Entities;

namespace TimeShare.App.Mappings;

public class MeetingMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Guid, HostId>()
            .MapWith(src => HostId.Create(src));
        config.NewConfig<(CreateMeetingRequest Request, Guid HostId), CreateMeetingCommand>()
            .Map(dest => dest.HostId, src => src.HostId)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<Meeting, MeetingResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.HostId, src => src.HostId.Value)
            //.Map(dest => dest.MeetingReviewIds, src => src.MeetingReviewIds.Select(id => id.Value))
            .Map(dest => dest.Invitations, src => src.Invitations);

        config.NewConfig<Invitation, InvitationResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.MeetingId, src => src.MeetingId.Value)
            .Map(dest => dest.GuestId, src => src.GuestId.Value);
    }
}