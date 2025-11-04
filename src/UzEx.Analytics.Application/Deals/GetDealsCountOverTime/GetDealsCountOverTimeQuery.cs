using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Deals.GetDealsCountOverTime;

public sealed record GetDealsCountOverTimeQuery(GetDealsCountOverTimeRequest Request)
    : ICachedQuery<GetDealsCountOverTimeResponse>
{
    public string CacheKey => $"deals-count-overTime-startDate-{Request.StartDate}-endDate-{Request.EndDate}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}

