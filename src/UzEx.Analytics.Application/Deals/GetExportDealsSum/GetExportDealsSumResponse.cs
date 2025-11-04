namespace UzEx.Analytics.Application.Deals.GetExportDealsSum;

public sealed class GetExportDealsSumResponse
{
    public decimal Sum { get; init; }

    public required string Unit { get; init; }
}