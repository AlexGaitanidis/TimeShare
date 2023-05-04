using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeShare.Domain.Aggregates.GuestAggregate;
using TimeShare.Domain.Aggregates.GuestAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.HostAggregate;
using TimeShare.Domain.Aggregates.HostAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.MeetingAggregate;
using TimeShare.Domain.Aggregates.MeetingAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.MeetingReviewAggregate;
using TimeShare.Domain.Aggregates.MeetingReviewAggregate.ValueObjects;
using TimeShare.Domain.Common.ValueObjects;

namespace TimeShare.Persistence.Configurations;

internal class MeetingReviewConfiguration : IEntityTypeConfiguration<MeetingReview>
{
    public void Configure(EntityTypeBuilder<MeetingReview> builder)
    {
        builder.ToTable("MeetingReviews");

        builder.HasKey(mr => mr.Id);

        builder.Property(mr => mr.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => MeetingReviewId.Create(value));

        builder.Property(mr => mr.Rating)
            .HasConversion(
                r => r.Value,
                value => Rating.Create(value));

        builder.Property(mr => mr.Comment).HasMaxLength(500);

        builder.Property(mr => mr.MeetingId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => MeetingId.Create(value));

        builder.HasOne<Meeting>()
            .WithMany()
            .HasForeignKey(mr => mr.MeetingId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(mr => mr.HostId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => HostId.Create(value));

        builder.HasOne<Host>()
            .WithMany()
            .HasForeignKey(mr => mr.HostId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(mr => mr.GuestId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => GuestId.Create(value));

        builder.HasOne<Guest>()
            .WithMany()
            .HasForeignKey(mr => mr.GuestId);
    }
}