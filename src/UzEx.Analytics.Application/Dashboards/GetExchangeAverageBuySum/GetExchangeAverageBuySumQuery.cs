using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Dashboards.GetExchangeAverageBuySum;

public sealed record GetExchangeAverageBuySumQuery(long Year, int Type) : ICachedQuery<GetExchangeAverageBuySumResponse>
{
    public string CacheKey => $"dash-exchange-avg-buy-sum-{Year}-{Type}";
    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}