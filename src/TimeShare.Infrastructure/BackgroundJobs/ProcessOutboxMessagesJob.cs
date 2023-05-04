using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Quartz;
using TimeShare.Domain.Common.Primitives;
using TimeShare.Persistence;
using TimeShare.Persistence.Infrastructure;

namespace TimeShare.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly TimeShareDbContext _dbContext;
    private readonly IPublisher _publisher;
    private readonly ILogger<ProcessOutboxMessagesJob> _logger;

    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All,
        ContractResolver = new PrivateResolver()
    };

    public ProcessOutboxMessagesJob(TimeShareDbContext dbContext, IPublisher publisher, ILogger<ProcessOutboxMessagesJob> logger)
    {
        _dbContext = dbContext;
        _publisher = publisher;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var outboxMessages = await _dbContext
            .OutboxMessages
            .Where(om => om.ProcessedOnUtc == null &&
                         om.Error == null)
            .Take(20)
            .ToListAsync(context.CancellationToken);

        foreach (var outboxMessage in outboxMessages)
        {
            IDomainEvent? domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(outboxMessage.Content, JsonSerializerSettings);

            if (domainEvent is null)
            {
                // TODO add proper logging, debug why getting null
                _logger.LogError("The content of the {@OutboxMessage} does not return a Domain Event, {@DateTimeUtc}",
                    outboxMessage,
                    DateTime.UtcNow);

                continue;
            }

            PolicyResult? policyResult = await Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(3, attempt => TimeSpan.FromMilliseconds(50 * attempt))
                .ExecuteAndCaptureAsync(() => 
                    _publisher.Publish(domainEvent, context.CancellationToken));

            outboxMessage.Error = policyResult.FinalException?.ToString();
            outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
        }

        await _dbContext.SaveChangesAsync();
    }
}