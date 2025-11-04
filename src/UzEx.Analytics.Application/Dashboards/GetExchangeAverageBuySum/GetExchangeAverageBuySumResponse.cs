namespace UzEx.Analytics.Application.Dashboards.GetExchangeAverageBuySum;

public sealed class GetExchangeAverageBuySumResponse
{
    public decimal Sum { get; init; }

    public required string SumUnit { get; init; }
}