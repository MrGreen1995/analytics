using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Deals.GetPaidDealsPercentage;

public sealed record GetPaidDealsPercentageQuery() : ICachedQuery<GetPaidDealsPercentageResponse>
{
    public string CacheKey => "deals-paid-percentage";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
