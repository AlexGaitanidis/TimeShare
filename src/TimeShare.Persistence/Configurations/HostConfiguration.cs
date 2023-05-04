using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeShare.Domain.Aggregates.HostAggregate;
using TimeShare.Domain.Aggregates.HostAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.MeetingAggregate.ValueObjects;
using TimeShare.Domain.Aggregates.UserAggregate;
using TimeShare.Domain.Aggregates.UserAggregate.ValueObjects;

namespace TimeShare.Persistence.Configurations;

internal class HostConfiguration : IEntityTypeConfiguration<Host>
{
    public void Configure(EntityTypeBuilder<Host> builder)
    {
        ConfigureHostsTable(builder);
        //ConfigureHostMeetingIdsTable(builder);
    }

    private static void ConfigureHostsTable(EntityTypeBuilder<Host> builder)
    {
        builder.ToTable("Hosts");

        builder.HasKey(h => h.Id);

        builder.Property(h => h.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => HostId.Create(value));

        builder.Property(g => g.FirstName).HasMaxLength(100);

        builder.Property(g => g.LastName).HasMaxLength(100);

        builder.OwnsOne(h => h.AverageRating, arb =>
        {
            arb.Property(ar => ar.Value).HasColumnName("AverageRating");
            arb.Property(ar => ar.RatingsCount).HasColumnName("RatingCount");
        });

        builder.Property(h => h.UserId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));

        builder.HasOne<User>().WithOne();

        builder.Ignore(h => h.MeetingIds);
    }

    private static void ConfigureHostMeetingIdsTable(EntityTypeBuilder<Host> builder)
    {
        builder.OwnsMany(h => h.MeetingIds, mib =>
        {
            mib.ToTable("HostMeetingIds");

            mib.WithOwner().HasForeignKey(nameof(HostId));

            mib.HasKey("Id");

            mib.Property(mi => mi.Value)
                .HasColumnName(nameof(MeetingId))
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Host.MeetingIds))?
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}