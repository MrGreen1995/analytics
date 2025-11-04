namespace UzEx.Analytics.Application.Deals.GetInternalDealsSum;

public sealed class GetInternalDealsSumResponse
{
    public decimal Sum { get; init; }

    public required string Unit { get; init; }
}