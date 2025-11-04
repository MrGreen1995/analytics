using UzEx.Analytics.Domain.Deals;

namespace UzEx.Analytics.Application.Brokers.GetBrokersLastDealsOverTime;

public sealed class GetBrokersLastDealsOverTimeResponse
{
    public Guid Id { get; init; }

    public string? Number { get; init; }

    public DateTime DateOnUtc { get; init; }

    public decimal Amount { get; init; }

    public decimal Price { get; init; }

    public decimal Cost { get; init; }

    public DealStatusType Status { get; init; }
}
