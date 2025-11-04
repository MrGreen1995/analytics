namespace UzEx.Analytics.Application.Dashboards.GetExchangeTotalBuyersCount;

public sealed class GetExchangeTotalBuyersCountResponse
{
    public decimal Count { get; init; }

    public required string CountUnit { get; init; }
}