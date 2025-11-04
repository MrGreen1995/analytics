using UzEx.Analytics.Application.Abstractions.Caching;
using UzEx.Analytics.Application.Abstractions.Messaging;

namespace UzEx.Analytics.Application.Deals.GetTotalDealsAmount;

public sealed record GetTotalDealsAmountQuery() : ICachedQuery<GetTotalDealsAmountResponse>
{
    public string CacheKey => "deals-total-amount";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
