using FluentValidation;

namespace TimeShare.Application.Meetings.Commands.CreateMeeting;

public class CreateMeetingCommandValidator : AbstractValidator<CreateMeetingCommand>
{
    public CreateMeetingCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty(); // TODO add better validations
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.StartOnUtc).GreaterThan(DateTime.UtcNow)
            .WithMessage("A meeting can not be created in the past.");
        RuleFor(x => x.EndOnUtc).GreaterThan(x => x.StartOnUtc)
            .WithMessage("The meeting's end time cannot be before the start time.");
        RuleFor(x => x.MaxGuests).GreaterThan(0);
        RuleFor(x => x.Location).SetValidator(new LocationCommandValidator()!);
    }
}

public class LocationCommandValidator : AbstractValidator<LocationCommand>
{
    public LocationCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Address).NotEmpty();
        RuleFor(x => x.Latitude).InclusiveBetween(-90, 90);
        RuleFor(x => x.Longitude).InclusiveBetween(-180, 180);
    }
}