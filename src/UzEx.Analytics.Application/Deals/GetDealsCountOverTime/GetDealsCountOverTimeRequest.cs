namespace UzEx.Analytics.Application.Deals.GetDealsCountOverTime;

public sealed class GetDealsCountOverTimeRequest
{
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}
