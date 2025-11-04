namespace UzEx.Analytics.Application.Orders.GetTotalOrdersCountByDirection;

public sealed class GetTotalOrdersCountByDirectionResponse
{
    public string DirectionType { get; init; } = string.Empty;

    public double Count { get; init; }
}
