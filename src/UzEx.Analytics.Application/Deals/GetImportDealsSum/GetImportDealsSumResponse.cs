namespace UzEx.Analytics.Application.Deals.GetImportDealsSum;

public sealed class GetImportDealsSumResponse
{
    public decimal Sum { get; init; }

    public required string Unit { get; init; }
}