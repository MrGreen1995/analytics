using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Deals.GetDealsCountTrendOverTime;

public sealed record GetDealsCountTrendOverTimeQuery(GetDealsCountTrendOverTimeRequest Request)
    : ICachedQuery<GetDealsCountTrendOverTimeResponse>
{
    public string CacheKey => $"deals-count-trend-overTime-startDate-{Request.StartDate}-endDate-{Request.EndDate}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
