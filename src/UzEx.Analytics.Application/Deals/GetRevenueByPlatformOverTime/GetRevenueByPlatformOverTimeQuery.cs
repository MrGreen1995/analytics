using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Deals.GetRevenueByPlatformOverTime;

public sealed record GetRevenueByPlatformOverTimeQuery(GetRevenueByPlatformOverTimeRequest Request)
    : ICachedQuery<List<GetRevenueByPlatformOverTimeResponse>>
{
    public string CacheKey => $"revenue-by-platform-overTime-startDate-{Request.StartDate}-endDate-{Request.EndDate}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
