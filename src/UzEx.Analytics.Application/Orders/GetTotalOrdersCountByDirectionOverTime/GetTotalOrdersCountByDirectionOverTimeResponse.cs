namespace UzEx.Analytics.Application.Orders.GetTotalOrdersCountByDirectionOverTime;

public sealed class GetTotalOrdersCountByDirectionOverTimeResponse
{
    public string DirectionType { get; init; } = string.Empty;

    public int Year { get; init; }

    public double TotalOrdersCount => Data.Sum(x => x.OrdersCount);

    public List<OrdersCountOfDirectionByMonthDataItem> Data { get; set; } = [];
}

public sealed class OrdersCountOfDirectionByMonthDataItem
{
    public string MonthName { get; set; } = string.Empty;

    public int MonthIndex { get; init; }

    public double OrdersCount { get; init; }
}
