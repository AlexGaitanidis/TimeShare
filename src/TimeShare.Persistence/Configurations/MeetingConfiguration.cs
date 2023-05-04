using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeShare.Domain.Aggregates.GuestAggregate;
using TimeShare.Domain.Aggregates.HostAggregate;
using TimeShare.Domain.Aggregates.GuestAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.HostAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.MeetingAggregate;
using TimeShare.Domain.Aggregates.MeetingAggregate.Entities;
using TimeShare.Domain.Aggregates.MeetingAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.MeetingReviewAggregate.ValueObjects;

namespace TimeShare.Persistence.Configurations;

internal class MeetingConfiguration : IEntityTypeConfiguration<Meeting>
{
    public void Configure(EntityTypeBuilder<Meeting> builder)
    {
        ConfigureMeetingsTable(builder);
        ConfigureInvitationsTable(builder);
        //ConfigureMeetingReviewIdsTable(builder);
    }

    private static void ConfigureMeetingsTable(EntityTypeBuilder<Meeting> builder)
    {
        builder.ToTable("Meetings");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => MeetingId.Create(value));

        builder.Property(m => m.Name).HasMaxLength(100);

        builder.Property(m => m.Description).HasMaxLength(500);

        builder.OwnsOne(m => m.AverageRating, arb =>
        {
            arb.Property(ar => ar.Value).HasColumnName("AverageRating");
            arb.Property(ar => ar.RatingsCount).HasColumnName("RatingsCount");
        });

        builder.Property(m => m.HostId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => HostId.Create(value));

        builder.HasOne<Host>()
            .WithMany()
            .HasForeignKey(m => m.HostId);

        builder.OwnsOne(m => m.Location, lb =>
        {
            lb.Property(l => l.Name).HasMaxLength(100).HasColumnName("LocationName");
            lb.Property(l => l.Address).HasMaxLength(100).HasColumnName("Address");
            lb.Property(l => l.Latitude).HasColumnName("Latitude");
            lb.Property(l => l.Longitude).HasColumnName("Longitude");
        });

        builder.Property(m => m.Status).HasConversion<string>().HasMaxLength(20);

        builder.Ignore(m => m.MeetingReviewIds);
    }

    private static void ConfigureInvitationsTable(EntityTypeBuilder<Meeting> builder)
    {
        builder.OwnsMany(m => m.Invitations, ib =>
        {
            ib.ToTable("Invitations");

            ib.WithOwner().HasForeignKey(nameof(MeetingId));

            ib.HasKey(nameof(Invitation.Id), nameof(MeetingId));

            ib.Property(i => i.Id)
                .HasColumnName(nameof(Invitation.Id))
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => InvitationId.Create(value));

            ib.Property(i => i.GuestId)
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => GuestId.Create(value));

            ib.HasOne<Guest>()
                .WithMany()
                .HasForeignKey(i => i.GuestId)
                .OnDelete(DeleteBehavior.NoAction);

            ib.Property(i => i.Status).HasConversion<string>().HasMaxLength(20);
        });

        builder.Metadata.FindNavigation(nameof(Meeting.Invitations))?
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureMeetingReviewIdsTable(EntityTypeBuilder<Meeting> builder)
    {
        builder.OwnsMany(m => m.MeetingReviewIds, mrb =>
        {
            mrb.ToTable("MeetingReviewIds");

            mrb.WithOwner().HasForeignKey(nameof(MeetingId));

            mrb.HasKey("Id");

            mrb.Property(mr => mr.Value)
                .HasColumnName(nameof(MeetingReviewId))
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Meeting.MeetingReviewIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}