namespace UzEx.Analytics.Application.Deals.GetRevenueByTradeTypeOverTime;

public sealed class GetRevenueByTradeTypeOverTimeRequest
{
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}
