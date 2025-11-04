using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Deals.GetAnnulatedDealsPercentage;

public sealed record GetAnnulatedDealsPercentageQuery() : ICachedQuery<GetAnnulatedDealsPercentageResponse>
{
    public string CacheKey => "deals-annulated-percentage";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
