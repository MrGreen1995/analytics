using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Deals.GetTotalDealsVolumeOverTime;

public sealed record GetTotalDealsVolumeOverTimeQuery(GetTotalDealsVolumeOverTimeRequest Request)
    : ICachedQuery<List<GetTotalDealsVolumeOverTimeResponse>>
{
    public string CacheKey => $"total-deals-volume-overTime-startDate-{Request.StartDate}-endDate-{Request.EndDate}";
    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
