using Microsoft.Extensions.Options;
using Quartz;

namespace UzEx.Analytics.Infrastructure.Outbox;

internal sealed class ProcessOutboxMessagesJobSetup : IConfigureOptions<QuartzOptions>
{
    private readonly OutboxOptions _options;

    public ProcessOutboxMessagesJobSetup(IOptions<OutboxOptions> options)
    {
        _options = options.Value;
    }

    public void Configure(QuartzOptions options)
    {
        const string jobName = nameof(ProcessOutboxMessagesJob);
        
        options.AddJob<ProcessOutboxMessagesJob>(configure => configure.WithIdentity(jobName))
            .AddTrigger(configure => configure.ForJob(jobName)
                .WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(_options.IntervalInSeconds)
                    .RepeatForever()));
    }
}