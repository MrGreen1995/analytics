namespace UzEx.Analytics.Application.Deals.GetRevenueByPlatformOverTime;

public sealed class GetRevenueByPlatformOverTimeRequest
{
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}
