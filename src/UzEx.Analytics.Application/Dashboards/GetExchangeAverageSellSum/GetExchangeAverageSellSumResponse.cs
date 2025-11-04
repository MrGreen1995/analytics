namespace UzEx.Analytics.Application.Dashboards.GetExchangeAverageSellSum;

public sealed class GetExchangeAverageSellSumResponse
{
    public decimal Sum { get; init; }

    public required string SumUnit { get; init; }
}