namespace UzEx.Analytics.Application.Brokers.GetBrokersLastOrdersOverTime;

public sealed class GetBrokersLastOrdersOverTimeRequest
{
    // Pagination
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;

    public Guid Id { get; init; }
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}
