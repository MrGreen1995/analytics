namespace UzEx.Analytics.Api.Controllers.Dashboards;

public sealed record GetExchangeVolumeRequest(
    int TradeType,
    DateOnly Start,
    DateOnly End,
    List<int> ContractTypes);