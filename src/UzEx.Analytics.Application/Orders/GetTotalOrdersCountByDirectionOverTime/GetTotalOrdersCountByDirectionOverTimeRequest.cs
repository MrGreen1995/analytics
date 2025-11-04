namespace UzEx.Analytics.Application.Orders.GetTotalOrdersCountByDirectionOverTime;

public sealed class GetTotalOrdersCountByDirectionOverTimeRequest
{
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}
