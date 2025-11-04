using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Orders.GetTotalOrdersCountByDirectionOverTime;

public sealed record GetTotalOrdersCountByDirectionOverTimeQuery(GetTotalOrdersCountByDirectionOverTimeRequest Request) :
    ICachedQuery<List<GetTotalOrdersCountByDirectionOverTimeResponse>>
{
    public string CacheKey => $"total-orders-count-by-direction-overTime-startDate-{Request.StartDate}-endDate-{Request.EndDate}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
