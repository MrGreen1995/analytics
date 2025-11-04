namespace UzEx.Analytics.Application.Deals.GetRevenueByPlatformOverTime;

public sealed class GetRevenueByPlatformOverTimeResponse
{
    public string Platform { get; init; } = string.Empty;

    public int Year { get; init; }

    public List<RevenueOfPlatformByMonthDataItem> Data { get; set; } = [];

    public double TotalDealsCount => Data.Sum(x => x.DealsCount);

    public decimal TotalDealsSum => Data.Sum(x => x.DealsSum);
}

public sealed class RevenueOfPlatformByMonthDataItem
{
    public string MonthName { get; init; } = string.Empty;

    public int MonthIndex { get; init; }

    public double DealsCount { get; init; }

    public decimal DealsSum { get; init; }
}