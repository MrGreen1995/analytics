namespace UzEx.Analytics.Application.Orders.GetTotalOrdersCountByDirection;

public sealed class GetTotalOrdersCountByDirectionRequest
{
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}
