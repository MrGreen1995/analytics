namespace UzEx.Analytics.Application.Brokers.GetTotalBrokersCountByRegionsOverTime;

public sealed class GetTotalBrokersCountByRegionsOverTimeRequest
{
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}
