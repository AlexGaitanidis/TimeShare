using FluentValidation;

namespace TimeShare.Application.Users.Commands.UpdateUserName;

public class UpdateUserNameCommandValidator : AbstractValidator<UpdateUserNameCommand>
{
    public UpdateUserNameCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
    }
}