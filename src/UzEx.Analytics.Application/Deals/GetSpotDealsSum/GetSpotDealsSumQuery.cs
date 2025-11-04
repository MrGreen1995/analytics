using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Deals.GetSpotDealsSum;

public sealed record GetSpotDealsSumQuery() : ICachedQuery<GetSpotDealsSumResponse>
{
    public string CacheKey => "deals-spot-sum";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
