namespace UzEx.Analytics.Application.Contracts.GetTotalContractsCountByTradeTypeOverTime;

public sealed class GetTotalContractsCountByTradeTypeOverTimeResponse
{
    public string TradeType { get; init; } = string.Empty;

    public int Year { get; init; }

    public double TotalOrdersCount => Data.Sum(c => c.OrdersCount);

    public List<ContractsCountOfDirectionByMonthDataItem> Data { get; set; } = [];
}

public sealed class ContractsCountOfDirectionByMonthDataItem
{
    public string MonthName { get; init; } = string.Empty;

    public int MonthIndex { get; init; }

    public double OrdersCount { get; init; }
}
