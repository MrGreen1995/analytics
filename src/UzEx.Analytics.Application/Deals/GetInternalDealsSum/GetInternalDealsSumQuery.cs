using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Deals.GetInternalDealsSum;

public sealed record GetInternalDealsSumQuery() : ICachedQuery<GetInternalDealsSumResponse>
{
    public string CacheKey => "deals-internal-sum";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}