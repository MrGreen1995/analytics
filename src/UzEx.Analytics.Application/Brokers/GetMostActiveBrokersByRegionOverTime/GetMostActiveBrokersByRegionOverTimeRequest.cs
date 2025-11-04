namespace UzEx.Analytics.Application.Brokers.GetMostActiveBrokersByRegionOverTime;

public sealed class GetMostActiveBrokersByRegionOverTimeRequest
{
    // Pagination
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;

    public int Region { get; init; }
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}
