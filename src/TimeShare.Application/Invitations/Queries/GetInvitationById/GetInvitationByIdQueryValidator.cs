using FluentValidation;

namespace TimeShare.Application.Invitations.Queries.GetInvitationById;

public class GetInvitationByIdQueryValidator : AbstractValidator<GetInvitationByIdQuery>
{
    public GetInvitationByIdQueryValidator()
    {
        RuleFor(x => x.MeetingId).NotEmpty();
        RuleFor(x => x.InvitationId).NotEmpty();
    }
}