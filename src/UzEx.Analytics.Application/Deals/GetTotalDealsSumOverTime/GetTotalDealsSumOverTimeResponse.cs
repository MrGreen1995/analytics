namespace UzEx.Analytics.Application.Deals.GetTotalDealsSumOverTime;

public sealed class GetTotalDealsSumOverTimeResponse
{
    public int Year { get; init; }

    public decimal TotalDealsSum => Data.Sum(x => x.DealsSum);

    public List<TotalDealsSumByMonthDataItem> Data { get; set; } = [];
}

public sealed class TotalDealsSumByMonthDataItem
{
    public string MonthName { get; init; } = string.Empty;

    public int MonthIndex { get; init; }

    public decimal DealsSum { get; init; }
}