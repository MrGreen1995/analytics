namespace UzEx.Analytics.Application.Deals.GetRevenueByTradeType;

public sealed class GetRevenueByTradeTypeRequest
{
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}
