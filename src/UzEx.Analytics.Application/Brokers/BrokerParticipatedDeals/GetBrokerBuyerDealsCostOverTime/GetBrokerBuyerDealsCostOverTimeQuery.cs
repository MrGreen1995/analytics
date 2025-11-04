using UzEx.Analytics.Application.Abstractions.Caching;
using UzEx.Analytics.Application.Brokers.BrokerParticipatedDeals.Shared;

namespace UzEx.Analytics.Application.Brokers.BrokerParticipatedDeals.GetBrokerBuyerDealsCostOverTime;

public sealed record GetBrokerBuyerDealsCostOverTimeQuery(GetBrokerHandledDealsCostOverTimeRequest Request)
    : ICachedQuery<List<GetBrokerHandledDealsCostOverTimeResponse>>
{
    public string CacheKey => $"broker-buyer-deals-cost-overTime-brokerId-{Request.Id}-startDate-{Request.StartDate}-endDate-{Request.EndDate}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
