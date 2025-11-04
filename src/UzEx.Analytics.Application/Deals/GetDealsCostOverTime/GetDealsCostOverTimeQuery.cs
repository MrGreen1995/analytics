using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Deals.GetDealsCostOverTime;

public sealed record GetDealsCostOverTimeQuery(GetDealsCostOverTimeRequest Request)
    : ICachedQuery<GetDealsCostOverTimeResponse>
{
    public string CacheKey => $"deals-sum-overTime-startDate-{Request.StartDate}-endDate-{Request.EndDate}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
