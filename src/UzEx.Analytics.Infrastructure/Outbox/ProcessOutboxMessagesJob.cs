using System.Data;
using Dapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quartz;
using UzEx.Analytics.Application.Abstractions.Clock;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.DataMigrations.ProcessDataMigrations;
using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Infrastructure.Outbox;

[DisallowConcurrentExecution]
internal sealed class ProcessOutboxMessagesJob : IJob
{
    private readonly ILogger<ProcessOutboxMessagesJob> _logger;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly OutboxOptions _options;
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    
    private readonly IServiceProvider _serviceProvider; 
    
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };
    
    private async Task<IReadOnlyList<OutboxMessageResponse>> GetOutboxMessagesAsync(
        IDbConnection connection,
        IDbTransaction transaction)
    {
        var sql = $"""
                      SELECT id, content, type
                      FROM outbox_messages
                      WHERE processed_on_utc IS NULL
                      ORDER BY occurred_on_utc
                      LIMIT {_options.BatchSize}
                      FOR UPDATE
                      """;

        var outboxMessages = await connection.QueryAsync<OutboxMessageResponse>(
            sql: sql, 
            transaction: transaction);

        return outboxMessages.ToList();
    }

    private async Task UpdateOutboxMessageAsync(
        IDbConnection connection,
        IDbTransaction transaction,
        OutboxMessageResponse outboxMessage,
        Exception? exception)
    {
        const string sql = """
                           UPDATE outbox_messages
                           SET processed_on_utc = @ProcessedOnUtc,
                               error = @Error
                           WHERE id = @Id
                           """;

        await connection.ExecuteAsync(
            sql,
            new
            {
                outboxMessage.Id,
                ProcessedOnUtc = _dateTimeProvider.UtcNow,
                Error = exception?.ToString()
            },
            transaction: transaction);
    }

    public ProcessOutboxMessagesJob(
        ILogger<ProcessOutboxMessagesJob> logger, 
        IDateTimeProvider dateTimeProvider, 
        IOptions<OutboxOptions> options, 
        ISqlConnectionFactory sqlConnectionFactory,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _dateTimeProvider = dateTimeProvider;
        _sqlConnectionFactory = sqlConnectionFactory;
        _options = options.Value;
        _serviceProvider = serviceProvider;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Begin to process outbox messages");

        using var connection = _sqlConnectionFactory.CreatePostgresConnection();
        using var transaction = connection.BeginTransaction();
        
        var outboxMessages = await GetOutboxMessagesAsync(connection, transaction);

        foreach (var outbox in outboxMessages)
        {
            Exception? exception = null;

            try
            {
                if (outbox.Type == "DataMigrationCreatedDomainEvent")
                {
                    var content = JsonConvert.DeserializeObject<OutboxMessageContent>(outbox.Content, JsonSerializerSettings);

                    using var scope = _serviceProvider.CreateScope();
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                    var result = await mediator.Send(new DataMigrationProcessQuery(content!.Id), context.CancellationToken);

                    if (!result.IsSuccess)
                    {
                        throw new Exception(result.Error.Message);
                    }
                }
                
                _logger.LogInformation("Outbox message {Id}", outbox.Id);
            }
            catch (Exception caughtException)
            {
                _logger.LogError(
                    caughtException,
                    "Exception while processing outbox message {MessageId}",
                    outbox.Id);

                exception = caughtException;
            }
            
            await UpdateOutboxMessageAsync(connection, transaction, outbox, exception);
        }
        
        transaction.Commit();
        
        _logger.LogInformation("Finish to process outbox messages");
        await Task.Delay(TimeSpan.FromSeconds(_options.IntervalInSeconds), context.CancellationToken);
    }
}