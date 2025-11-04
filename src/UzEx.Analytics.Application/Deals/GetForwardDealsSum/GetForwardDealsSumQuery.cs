using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Deals.GetForwardDealsSum;

public sealed record GetForwardDealsSumQuery() : ICachedQuery<GetForwardDealsSumResponse>
{
    public string CacheKey => "deals-forward-sum";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
