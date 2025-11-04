namespace UzEx.Analytics.Application.Deals.GetRevenueByContractTypeOverTime;

public sealed class GetRevenueByContractTypeOverTimeRequest
{
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}
