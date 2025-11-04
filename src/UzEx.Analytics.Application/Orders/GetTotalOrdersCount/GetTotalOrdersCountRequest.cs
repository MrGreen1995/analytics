namespace UzEx.Analytics.Application.Orders.GetTotalOrdersCount;

public sealed class GetTotalOrdersCountRequest
{
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}
