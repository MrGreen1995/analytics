using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Deals.GetRevenueByContractFormOverTime;

public sealed record GetRevenueByContractFormOverTimeQuery(GetRevenueByContractFormOverTimeRequest Request)
    : ICachedQuery<List<GetRevenueByContractFormOverTimeResponse>>
{
    public string CacheKey => $"revenue-by-contract-form-overTime-startDate-{Request.StartDate}-endDate-{Request.EndDate}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
