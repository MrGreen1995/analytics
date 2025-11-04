using UzEx.Analytics.Domain.Clients;

namespace UzEx.Analytics.Application.Brokers.BrokerClients.Shared;

public sealed class GetBrokerClientsOverTimeResponse
{
    public Guid Id { get; init; }

    public ClientType Type { get; init; }

    public string? RegNumber { get; init; }

    public string? Name { get; init; }

    public string? Country { get; init; }

    public string? Region { get; init; }

    public string? District { get; init; }

    public string? Address { get; init; }

    public int DealsCount { get; init; }

    public decimal DealsSum { get; init; }
}
