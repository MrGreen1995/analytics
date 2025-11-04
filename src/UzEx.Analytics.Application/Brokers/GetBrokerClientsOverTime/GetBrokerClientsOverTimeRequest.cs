namespace UzEx.Analytics.Application.Brokers.GetBrokerClientsOverTime;

public sealed class GetBrokerClientsOverTimeRequest
{
    // Pagination
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;

    public Guid BrokerId { get; init; }
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}
