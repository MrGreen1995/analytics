namespace UzEx.Analytics.Application.Deals.GetTotalDealsAmount;

public sealed class GetTotalDealsAmountResponse
{
    public decimal Amount { get; init; }
    
    public required string Unit { get; init; }
}