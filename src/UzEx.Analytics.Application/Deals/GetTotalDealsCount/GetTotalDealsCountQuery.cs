using UzEx.Analytics.Application.Abstractions.Caching;
using UzEx.Analytics.Application.Abstractions.Messaging;

namespace UzEx.Analytics.Application.Deals.GetTotalDealsCount;

public sealed record GetTotalDealsCountQuery() : ICachedQuery<GetTotalDealsCountResponse>
{
    public string CacheKey => "deals-total-count";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
