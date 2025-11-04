namespace UzEx.Analytics.Api.Controllers.Dashboards;

public sealed record GetExchangeParticipantsRequest(
    int TradeType,
    DateOnly Start,
    DateOnly End,
    List<int> ContractTypes);