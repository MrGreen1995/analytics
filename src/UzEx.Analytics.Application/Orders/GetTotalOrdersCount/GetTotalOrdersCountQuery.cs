using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Orders.GetTotalOrdersCount;

public sealed record GetTotalOrdersCountQuery(GetTotalOrdersCountRequest Request) : ICachedQuery<GetTotalOrdersCountResponse>
{
    public string CacheKey => $"total-orders-count-startDate-{Request.StartDate}-endDate-{Request.EndDate}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
