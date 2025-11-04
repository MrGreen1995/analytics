namespace UzEx.Analytics.Application.Deals.GetSpotDealsSum;

public sealed class GetSpotDealsSumResponse
{
    public decimal Sum { get; init; }

    public required string Unit { get; init; }
}