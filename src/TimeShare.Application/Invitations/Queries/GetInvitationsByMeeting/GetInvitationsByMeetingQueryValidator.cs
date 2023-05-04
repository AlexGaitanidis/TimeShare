using FluentValidation;

namespace TimeShare.Application.Invitations.Queries.GetInvitationsByMeeting;

public class GetInvitationsByMeetingQueryValidator : AbstractValidator<GetInvitationsByMeetingQuery>
{
    public GetInvitationsByMeetingQueryValidator()
    {
        RuleFor(x => x.MeetingId).NotEmpty();
    }
}