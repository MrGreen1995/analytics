namespace UzEx.Analytics.Application.Brokers.GetMostPassiveBrokersByRegionOverTime;

public sealed class GetMostPassiveBrokersByRegionOverTimeRequest
{
    // Pagination
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;

    public int Region { get; init; }
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}
