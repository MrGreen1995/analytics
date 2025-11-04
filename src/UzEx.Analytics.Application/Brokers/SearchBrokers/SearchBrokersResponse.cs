namespace UzEx.Analytics.Application.Brokers.SearchBrokers;

public sealed class SearchBrokersResponse
{
    public Guid Id { get; init; }
    public DateTime CreatedOnUtc { get; init; }

    public string? BusinessKey { get; init; }

    public string? RegNumber { get; init; }

    public string? Name { get; init; }

    public string? Number { get; init; }

    public string? Region { get; init; }
}
