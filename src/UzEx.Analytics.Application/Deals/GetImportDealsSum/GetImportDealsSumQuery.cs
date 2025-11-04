using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Deals.GetImportDealsSum;

public sealed record GetImportDealsSumQuery() : ICachedQuery<GetImportDealsSumResponse>
{
    public string CacheKey => "deals-import-sum";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
