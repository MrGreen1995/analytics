using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Orders.GetTotalOrdersCountByDirection;

public sealed record GetTotalOrdersCountByDirectionQuery(GetTotalOrdersCountByDirectionRequest Request) :
    ICachedQuery<List<GetTotalOrdersCountByDirectionResponse>>
{
    public string CacheKey => $"total-orders-count-by-direction-startDate-{Request.StartDate}-endDate-{Request.EndDate}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
