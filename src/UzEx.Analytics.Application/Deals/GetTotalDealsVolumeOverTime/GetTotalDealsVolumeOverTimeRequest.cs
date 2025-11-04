namespace UzEx.Analytics.Application.Deals.GetTotalDealsVolumeOverTime;

public sealed class GetTotalDealsVolumeOverTimeRequest
{
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}
