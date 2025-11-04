namespace UzEx.Analytics.Application.Deals.GetForwardDealsSum;

public sealed class GetForwardDealsSumResponse
{
    public decimal Sum { get; init; }

    public required string Unit { get; init; }
}