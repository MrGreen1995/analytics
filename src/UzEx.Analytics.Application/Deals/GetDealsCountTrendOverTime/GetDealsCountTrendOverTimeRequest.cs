namespace UzEx.Analytics.Application.Deals.GetDealsCountTrendOverTime;

public sealed class GetDealsCountTrendOverTimeRequest
{
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}
