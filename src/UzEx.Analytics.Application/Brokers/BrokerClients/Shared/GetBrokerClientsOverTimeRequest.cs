namespace UzEx.Analytics.Application.Brokers.BrokerClients.Shared;

public sealed class GetBrokerClientsOverTimeRequest
{
    // Pagination
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;

    public Guid Id { get; init; }
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}
