namespace UzEx.Analytics.Application.Deals.GetTotalDealsCount;

public sealed class GetTotalDealsCountResponse
{
    public decimal Count { get; init; }

    public required string Unit { get; init; }
}