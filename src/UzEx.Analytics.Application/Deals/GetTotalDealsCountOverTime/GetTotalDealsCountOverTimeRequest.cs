namespace UzEx.Analytics.Application.Deals.GetTotalDealsCountOverTime;

public sealed class GetTotalDealsCountOverTimeRequest
{
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}
