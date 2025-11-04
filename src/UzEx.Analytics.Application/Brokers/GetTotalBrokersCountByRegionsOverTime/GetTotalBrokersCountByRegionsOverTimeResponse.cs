namespace UzEx.Analytics.Application.Brokers.GetTotalBrokersCountByRegionsOverTime;

public sealed class GetTotalBrokersCountByRegionsOverTimeResponse
{
    public string RegionIndex { get; init; } = string.Empty;

    public int BrokersCount { get; init; }

    public double DealsCount { get; init; }

    public decimal DealsSum { get; init; }
}
