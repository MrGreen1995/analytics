using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Deals.GetFutureDealsSum;

public sealed record GetFutureDealsSumQuery() : ICachedQuery<GetFutureDealsSumResponse>
{
    public string CacheKey => "deals-future-sum";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
