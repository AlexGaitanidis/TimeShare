using TimeShare.Domain.Aggregates.MeetingReviewAggregate;
using TimeShare.Domain.Aggregates.MeetingReviewAggregate.ValueObjects;

namespace TimeShare.Application.Abstractions.Persistence;

public interface IMeetingReviewRepository : IRepository<MeetingReview, MeetingReviewId>
{
}