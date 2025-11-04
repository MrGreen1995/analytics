using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Dashboards.GetExchangeTotalSellersCount;

public record GetExchangeTotalSellersCountQuery(long Year, int Type) : ICachedQuery<GetExchangeTotalSellersCountResponse>
{
    public string CacheKey => $"dash-exchange-total-sellers-count-{Year}-{Type}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
};