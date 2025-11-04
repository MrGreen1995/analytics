namespace UzEx.Analytics.Application.Deals.GetTotalDealsSum;

public sealed class GetTotalDealsSumResponse
{
    public decimal Sum { get; init; }

    public required string Unit { get; init; }
}