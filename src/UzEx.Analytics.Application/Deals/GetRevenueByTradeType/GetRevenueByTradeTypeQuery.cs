using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Deals.GetRevenueByTradeType;

public sealed record GetRevenueByTradeTypeQuery(GetRevenueByTradeTypeRequest Request) : ICachedQuery<List<GetRevenueByTradeTypeResponse>>
{
    public string CacheKey => $"revenue-by-trade-type-startDate-{Request.StartDate}-endDate-{Request.EndDate}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
