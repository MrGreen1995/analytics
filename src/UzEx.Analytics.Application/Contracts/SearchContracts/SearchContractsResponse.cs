namespace UzEx.Analytics.Application.Contracts.SearchContracts;

public sealed class SearchContractsResponse
{
    public Guid Id { get; init; }

    public string? BusinessKey { get; init; }

    public string? Number { get; init; }

    public string? Type { get; init; }

    public string? Form { get; init; }

    public string? TradeType { get; init; }

    public string? Currency { get; init; }

    public string? Warehouse { get; init; }

    public decimal? Lot { get; init; }

    public string? Unit { get; init; }

    public decimal? BasePrice { get; init; }
}