using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Deals.GetRevenueByContractTypeOverTime;

public sealed record GetRevenueByContractTypeOverTimeQuery(GetRevenueByContractTypeOverTimeRequest Request)
    : ICachedQuery<List<GetRevenueByContractTypeOverTimeResponse>>
{
    public string CacheKey => $"revenue-by-contract-type-overTime-startDate-{Request.StartDate}-endDate-{Request.EndDate}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
