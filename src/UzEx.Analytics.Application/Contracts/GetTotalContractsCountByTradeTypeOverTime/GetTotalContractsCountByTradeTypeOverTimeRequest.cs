namespace UzEx.Analytics.Application.Contracts.GetTotalContractsCountByTradeTypeOverTime;

public sealed class GetTotalContractsCountByTradeTypeOverTimeRequest
{
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}
