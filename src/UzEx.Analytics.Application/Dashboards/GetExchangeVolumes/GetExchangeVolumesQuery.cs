using UzEx.Analytics.Application.Abstractions.Caching;
using UzEx.Analytics.Domain.Contracts;

namespace UzEx.Analytics.Application.Dashboards.GetExchangeVolumes;

public sealed record GetExchangeVolumesQuery(
    ContractTradeType TradeType,
    DateOnly StartDate,
    DateOnly EndDate,
    List<ContractType> ContractTypes) : ICachedQuery<GetExchangeVolumesResponse>
{
    public string CacheKey =>
        $"dash-exchange-volumes-{TradeType}-{StartDate:yyyyMMdd}-{EndDate:yyyyMMdd}-{string.Join("_", ContractTypes.OrderBy(x => x))}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
