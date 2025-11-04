namespace UzEx.Analytics.Application.Brokers.GetMostActiveBrokersByRegionOverTime;

public sealed class GetMostActiveBrokersByRegionOverTimeResponse
{
    public Guid Id { get; init; }

    public string Region { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    public string RegNumber { get; init; } = string.Empty;

    public string BusinessKey { get; init; } = string.Empty;

    public int SellerDealsCount { get; init; }

    public int BuyerDealsCount { get; init; }

    public decimal SellerDealsSum { get; init; }

    public decimal BuyerDealsSum { get; init; }
}
