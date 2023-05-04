using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TimeShare.Persistence.Outbox;

namespace TimeShare.Persistence.Configurations;

internal class OutboxMessageConsumerConfiguration : IEntityTypeConfiguration<OutboxMessageConsumer>
{
    public void Configure(EntityTypeBuilder<OutboxMessageConsumer> builder)
    {
        builder.ToTable("OutboxMessageConsumers");

        builder.HasKey(x => new { x.Id, x.Name });

        builder.Property(x => x.Name).HasMaxLength(100);
    }
}