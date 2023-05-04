using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeShare.Persistence.Outbox;

namespace TimeShare.Persistence.Configurations;

internal class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("OutboxMessages");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Type).HasMaxLength(100);

        builder.Property(x => x.Content).HasMaxLength(500);

        builder.Property(x => x.Error).HasMaxLength(500);
    }
}