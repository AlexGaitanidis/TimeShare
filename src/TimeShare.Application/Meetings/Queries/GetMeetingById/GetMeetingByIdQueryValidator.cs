using FluentValidation;

namespace TimeShare.Application.Meetings.Queries.GetMeetingById;

public class GetMeetingByIdQueryValidator : AbstractValidator<GetMeetingByIdQuery>
{
    public GetMeetingByIdQueryValidator()
    {
        RuleFor(x => x.MeetingId).NotEmpty();
    }
}