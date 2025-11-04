namespace UzEx.Analytics.Application.Deals.GetRevenueByContractFormOverTime;

public sealed class GetRevenueByContractFormOverTimeRequest
{
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}
