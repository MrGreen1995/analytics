namespace UzEx.Analytics.Infrastructure.Outbox;

public sealed record OutboxMessageResponse(Guid Id, string Content, string Type);