using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Contracts.GetTotalContractsCountByTradeTypeOverTime;

public sealed record GetTotalContractsCountByTradeTypeOverTimeQuery(GetTotalContractsCountByTradeTypeOverTimeRequest Request) :
    ICachedQuery<List<GetTotalContractsCountByTradeTypeOverTimeResponse>>
{
    public string CacheKey => $"total-contracts-count-by-trade-type-overTime-startDate-{Request.StartDate}-endDate-{Request.EndDate}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
