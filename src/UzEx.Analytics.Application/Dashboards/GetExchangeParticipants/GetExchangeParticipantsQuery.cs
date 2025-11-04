using UzEx.Analytics.Application.Abstractions.Caching;
using UzEx.Analytics.Domain.Contracts;

namespace UzEx.Analytics.Application.Dashboards.GetExchangeParticipants;

public sealed record GetExchangeParticipantsQuery(
    ContractTradeType TradeType,
    DateOnly StartDate,
    DateOnly EndDate,
    List<ContractType> ContractTypes)
: ICachedQuery<GetExchangeParticipantsResponse>
{
    public string CacheKey =>
        $"dash-exchange-participants-{TradeType}-{StartDate:yyyyMMdd}-{EndDate:yyyyMMdd}-{string.Join("_", ContractTypes.OrderBy(x => x))}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
