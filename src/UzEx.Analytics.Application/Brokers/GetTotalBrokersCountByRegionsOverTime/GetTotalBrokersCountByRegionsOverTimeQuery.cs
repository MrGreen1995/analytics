using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Brokers.GetTotalBrokersCountByRegionsOverTime;

public sealed record GetTotalBrokersCountByRegionsOverTimeQuery(GetTotalBrokersCountByRegionsOverTimeRequest Request)
    : ICachedQuery<List<GetTotalBrokersCountByRegionsOverTimeResponse>>
{
    public string CacheKey => $"total-brokers-count-overTime-startDate-{Request.StartDate}-endDate-{Request.EndDate}";
    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
