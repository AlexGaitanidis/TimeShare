using TimeShare.Application.Abstractions.Persistence;
using TimeShare.Domain.Aggregates.MeetingReviewAggregate;
using TimeShare.Domain.Aggregates.MeetingReviewAggregate.ValueObjects;

namespace TimeShare.Persistence.Repositories;

public class MeetingReviewRepository : Repository<MeetingReview, MeetingReviewId>, IMeetingReviewRepository
{
    public MeetingReviewRepository(TimeShareDbContext context) : base(context)
    {
    }
}