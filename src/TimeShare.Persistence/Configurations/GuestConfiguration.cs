using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeShare.Domain.Aggregates.GuestAggregate;
using TimeShare.Domain.Aggregates.GuestAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.MeetingAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.MeetingReviewAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.UserAggregate;
using TimeShare.Domain.Aggregates.UserAggregate.ValueObjects;

namespace TimeShare.Persistence.Configurations;

internal class GuestConfiguration : IEntityTypeConfiguration<Guest>
{
    public void Configure(EntityTypeBuilder<Guest> builder)
    {
        ConfigureGuestsTable(builder);
        //ConfigureGuestMeetingIdsTable(builder);
        //ConfigureGuestMeetingReviewIdsTable(builder);
    }

    private static void ConfigureGuestsTable(EntityTypeBuilder<Guest> builder)
    {
        builder.ToTable("Guests");

        builder.HasKey(g => g.Id);

        builder.Property(g => g.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => GuestId.Create(value));

        builder.Property(g => g.FirstName).HasMaxLength(100);

        builder.Property(g => g.LastName).HasMaxLength(100);

        builder.OwnsOne(g => g.AverageRating, arb =>
        {
            arb.Property(ar => ar.Value).HasColumnName("AverageRating");
            arb.Property(ar => ar.RatingsCount).HasColumnName("RatingCount");
        });

        builder.Property(g => g.UserId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));

        builder.HasOne<User>().WithOne();

        builder.Ignore(g => g.MeetingReviewIds);

        builder.Ignore(g => g.UpcomingMeetingIds);

        builder.Ignore(g => g.PendingMeetingIds);

        builder.Ignore(g => g.PastMeetingIds);
    }

    private static void ConfigureGuestMeetingIdsTable(EntityTypeBuilder<Guest> builder)
    {
        builder.OwnsMany(g => g.UpcomingMeetingIds, umb =>
        {
            umb.ToTable("GuestUpcomingMeetingIds");

            umb.WithOwner().HasForeignKey(nameof(GuestId));

            umb.HasKey("Id");

            umb.Property(mi => mi.Value)
                .HasColumnName(nameof(MeetingId))
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Guest.UpcomingMeetingIds))?
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.OwnsMany(g => g.PastMeetingIds, pmb =>
        {
            pmb.ToTable("GuestPastMeetingIds");

            pmb.WithOwner().HasForeignKey(nameof(GuestId));

            pmb.HasKey("Id");

            pmb.Property(mi => mi.Value)
                .HasColumnName(nameof(MeetingId))
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Guest.PastMeetingIds))?
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.OwnsMany(g => g.PendingMeetingIds, pmb =>
        {
            pmb.ToTable("GuestPendingMeetingIds");

            pmb.WithOwner().HasForeignKey(nameof(GuestId));

            pmb.HasKey("Id");

            pmb.Property(mi => mi.Value)
                .HasColumnName(nameof(MeetingId))
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Guest.PendingMeetingIds))?
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureGuestMeetingReviewIdsTable(EntityTypeBuilder<Guest> builder)
    {
        builder.OwnsMany(g => g.MeetingReviewIds, mrb =>
        {
            mrb.ToTable("GuestMeetingReviewIds");

            mrb.WithOwner().HasForeignKey(nameof(GuestId));

            mrb.HasKey("Id");

            mrb.Property(mi => mi.Value)
                .HasColumnName(nameof(MeetingReviewId))
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Guest.MeetingReviewIds))?
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}