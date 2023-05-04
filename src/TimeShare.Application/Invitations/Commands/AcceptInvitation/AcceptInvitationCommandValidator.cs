using FluentValidation;

namespace TimeShare.Application.Invitations.Commands.AcceptInvitation;

public class AcceptInvitationCommandValidator : AbstractValidator<AcceptInvitationCommand>
{
    public AcceptInvitationCommandValidator()
    {
        RuleFor(x => x.InvitationId).NotEmpty();
        RuleFor(x => x.MeetingId).NotEmpty();
    }
}