using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Deals.GetTotalDealsCountOverTime;

public sealed record GetTotalDealsCountOverTimeQuery(GetTotalDealsCountOverTimeRequest Request)
   : ICachedQuery<List<GetTotalDealsCountOverTimeResponse>>
{
    public string CacheKey => $"total-deals-count-overTime-startDate-{Request.StartDate}-endDate-{Request.EndDate}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
