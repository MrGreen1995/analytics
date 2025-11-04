using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Deals.GetExportDealsSum;

public sealed record GetExportDealsSumQuery() : ICachedQuery<GetExportDealsSumResponse>
{
    public string CacheKey => "deals-export-sum";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
