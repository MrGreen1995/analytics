using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Dashboards.GetExchangeAverageSellSum;

public sealed record GetExchangeAverageSellSumQuery(long Year, int Type) : ICachedQuery<GetExchangeAverageSellSumResponse>
{
    public string CacheKey => $"dash-exchange-avg-sell-sum-{Year}-{Type}";
    
    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
};