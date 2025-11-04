namespace UzEx.Analytics.Application.Contracts.GetContract;

public sealed class GetContractResponse
{
    public Guid Id { get; init; }

    public string? BusinessKey { get; init; }

    public string? Number { get; init; }

    public string? Type { get; init; }

    public string? Form { get; init; }

    public decimal? Lot { get; init; }

    public string? Unit { get; init; }

    public decimal? BasePrice { get; init; }
}