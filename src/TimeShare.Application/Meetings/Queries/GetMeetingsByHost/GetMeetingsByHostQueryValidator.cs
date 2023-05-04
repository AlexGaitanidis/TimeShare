using FluentValidation;

namespace TimeShare.Application.Meetings.Queries.GetMeetingsByHost;

public class GetMeetingsByHostQueryValidator : AbstractValidator<GetMeetingsByHostQuery>
{
    public GetMeetingsByHostQueryValidator()
    {
        RuleFor(x => x.HostId).NotEmpty();
    }
}