namespace UzEx.Analytics.Application.Deals.GetTotalDealsSumOverTime;

public sealed class GetTotalDealsSumOverTimeRequest
{
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}
