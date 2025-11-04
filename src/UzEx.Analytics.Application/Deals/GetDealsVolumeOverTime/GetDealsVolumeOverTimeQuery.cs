using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Deals.GetDealsVolumeOverTime;

public sealed record GetDealsVolumeOverTimeQuery(GetDealsVolumeOverTimeRequest Request)
    : ICachedQuery<GetDealsVolumeOverTimeResponse>
{
    public string CacheKey => $"deals-volume-overTime-startDate-{Request.StartDate}-endDate-{Request.EndDate}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
