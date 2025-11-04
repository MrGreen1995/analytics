using UzEx.Analytics.Domain.Clients;

namespace UzEx.Analytics.Application.Clients.GetTopSellersOverTime;

public sealed class GetTopSellersOverTimeResponse
{
    public Guid Id { get; init; }

    public ClientType Type { get; init; }

    public string? RegNumber { get; init; }

    public string? Name { get; init; }

    public string? Country { get; init; }

    public string? Region { get; init; }

    public int TotalDealsCount { get; init; }

    public decimal TotalDealsCost { get; init; }
}
