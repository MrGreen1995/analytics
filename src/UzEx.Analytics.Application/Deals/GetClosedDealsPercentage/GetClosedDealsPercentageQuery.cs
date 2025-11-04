using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Deals.GetClosedDealsPercentage;

public sealed record GetClosedDealsPercentageQuery() : ICachedQuery<GetClosedDealsPercentageResponse>
{
    public string CacheKey => "deals-closed-percentage";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
