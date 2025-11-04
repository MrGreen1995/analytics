namespace UzEx.Analytics.Application.Deals.GetTotalDealsCountOverTime;

public sealed class GetTotalDealsCountOverTimeResponse
{
    public int Year { get; init; }

    public double TotalDealsCount => Data.Sum(x => x.DealsCount);

    public List<TotalDealsCountByMonthDataItem> Data { get; set; } = [];
}

public sealed class TotalDealsCountByMonthDataItem
{
    public string MonthName { get; init; } = string.Empty;

    public int MonthIndex { get; init; }

    public double DealsCount { get; init; }
}