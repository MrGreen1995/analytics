namespace UzEx.Analytics.Application.Deals.GetDealsCostOverTime;

public sealed class GetDealsCostOverTimeRequest
{
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}
