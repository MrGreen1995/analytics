namespace UzEx.Analytics.Infrastructure.Outbox;

public sealed class OutboxMessage
{
    public Guid Id { get; init; }

    public DateTime OccurredOnUtc { get; init; }

    public string Type { get; init; }

    public string Content { get; init; }

    public DateTime? ProcessedOnUtc { get; init; }

    public string? Error { get; init; }
    
    private OutboxMessage()
    {
    }
    
    public OutboxMessage(Guid id, DateTime occuredOnUtc, string type, string content)
    {
        Id = id;
        OccurredOnUtc = occuredOnUtc;
        Content = content;
        Type = type;
    }
}