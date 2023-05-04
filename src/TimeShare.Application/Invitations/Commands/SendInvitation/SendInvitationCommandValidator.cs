using FluentValidation;

namespace TimeShare.Application.Invitations.Commands.SendInvitation;

public class SendInvitationCommandValidator : AbstractValidator<SendInvitationCommand>
{
    public SendInvitationCommandValidator()
    {
        RuleFor(x => x.MeetingId).NotEmpty();
        RuleFor(x => x.GuestId).NotEmpty();
    }
}