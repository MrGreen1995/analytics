using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Deals.GetRevenueByTradeTypeOverTime;

public sealed record GetRevenueByTradeTypeOverTimeQuery(GetRevenueByTradeTypeOverTimeRequest Request) :
    ICachedQuery<List<GetRevenueByTradeTypeOverTimeResponse>>
{
    public string CacheKey => $"revenue-by-tradeType-overTime-startDate-{Request.StartDate}-endDate-{Request.EndDate}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
