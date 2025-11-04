using UzEx.Analytics.Application.Models.Shared;

namespace UzEx.Analytics.Application.Brokers.SearchBrokers;

public sealed record SearchBrokersRequest : PaginationAndSorting
{
    // Filter Parameters
    public DateOnly? From { get; init; }
    public DateOnly? To { get; init; }

    public string? BusinessKey { get; init; }

    public string? RegNumber { get; init; }

    public string? Name { get; init; }

    public string? Number { get; init; }

    public string? Region { get; init; }
}
