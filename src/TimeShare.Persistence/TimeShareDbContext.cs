using Microsoft.EntityFrameworkCore;
using TimeShare.Domain.Aggregates.HostAggregate;
using TimeShare.Domain.Aggregates.GuestAggregate;
using TimeShare.Domain.Aggregates.MeetingAggregate;
using TimeShare.Domain.Aggregates.MeetingReviewAggregate;
using TimeShare.Domain.Aggregates.UserAggregate;
using TimeShare.Persistence.Outbox;

namespace TimeShare.Persistence;

public sealed class TimeShareDbContext : DbContext
{
    public TimeShareDbContext(DbContextOptions options) 
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Host> Hosts { get; set; } = null!;
    public DbSet<Guest> Guests { get; set; } = null!;
    public DbSet<Meeting> Meetings { get; set; } = null!;
    public DbSet<MeetingReview> MeetingReviews { get; set; } = null!;
    public DbSet<OutboxMessage> OutboxMessages { get; set; } = null!;
    public DbSet<OutboxMessageConsumer> OutboxMessageConsumers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TimeShareDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}