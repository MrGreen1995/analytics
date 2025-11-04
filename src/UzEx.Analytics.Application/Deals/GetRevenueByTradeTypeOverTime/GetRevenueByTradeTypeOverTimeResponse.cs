namespace UzEx.Analytics.Application.Deals.GetRevenueByTradeTypeOverTime;

public sealed class GetRevenueByTradeTypeOverTimeResponse
{
    public string TradeType { get; init; } = string.Empty;

    public int Year { get; init; }

    public List<RevenueOfTradeTypeByMonthDataItem> Data { get; set; } = [];

    public double TotalDealsCount => Data.Sum(x => x.DealsCount);

    public decimal TotalDealsSum => Data.Sum(x => x.DealsSum);
}

public sealed class RevenueOfTradeTypeByMonthDataItem
{
    public string MonthName { get; init; } = string.Empty;

    public int MonthIndex { get; init; }

    public double DealsCount { get; init; }

    public decimal DealsSum { get; init; }
}
