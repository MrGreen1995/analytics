using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Deals.GetTotalDealsSum;

public sealed record GetTotalDealsSumQuery() : ICachedQuery<GetTotalDealsSumResponse>
{
    public string CacheKey => $"deals-total-sum";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
};