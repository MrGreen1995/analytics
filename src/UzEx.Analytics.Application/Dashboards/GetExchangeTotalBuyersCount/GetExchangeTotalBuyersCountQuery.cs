using UzEx.Analytics.Application.Abstractions.Caching;
using UzEx.Analytics.Application.Abstractions.Messaging;

namespace UzEx.Analytics.Application.Dashboards.GetExchangeTotalBuyersCount;

public sealed record GetExchangeTotalBuyersCountQuery(long Year, int Type) : ICachedQuery<GetExchangeTotalBuyersCountResponse>
{
    public string CacheKey => $"dash-exchange-total-buyers-count-{Year}-{Type}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
};