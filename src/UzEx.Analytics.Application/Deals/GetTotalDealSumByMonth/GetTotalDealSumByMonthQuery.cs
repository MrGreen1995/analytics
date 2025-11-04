using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Deals.GetTotalDealSumByMonth;

public sealed record GetTotalDealSumByMonthQuery(long Year) : ICachedQuery<IReadOnlyList<GetTotalDealSumByMonthResponse>>
{
    public string CacheKey => $"deals-sum-by-month-{Year}";
    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
};