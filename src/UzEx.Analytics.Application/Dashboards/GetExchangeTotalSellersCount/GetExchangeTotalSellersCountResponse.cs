namespace UzEx.Analytics.Application.Dashboards.GetExchangeTotalSellersCount;

public sealed class GetExchangeTotalSellersCountResponse
{
    public decimal Count { get; init; }

    public required string CountUnit { get; init; }
}