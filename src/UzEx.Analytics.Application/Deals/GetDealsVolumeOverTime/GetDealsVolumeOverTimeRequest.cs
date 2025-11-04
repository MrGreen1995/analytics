namespace UzEx.Analytics.Application.Deals.GetDealsVolumeOverTime;

public sealed class GetDealsVolumeOverTimeRequest
{
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}
