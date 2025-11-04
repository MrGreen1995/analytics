using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Deals.GetTotalDealsSumOverTime;

public sealed record GetTotalDealsSumOverTimeQuery(GetTotalDealsSumOverTimeRequest Request)
    : ICachedQuery<List<GetTotalDealsSumOverTimeResponse>>
{
    public string CacheKey => $"total-deals-sum-overTime-startDate-{Request.StartDate}-endDate-{Request.EndDate}";
    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
