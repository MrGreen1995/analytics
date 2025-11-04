namespace UzEx.Analytics.Application.Deals.GetTotalDealsVolumeOverTime;

public sealed class GetTotalDealsVolumeOverTimeResponse
{
    public int Year { get; init; }

    public decimal TotalDealsVolume => Data.Sum(x => x.DealsVolume);

    public List<TotalDealsVolumeByMonthDataItem> Data { get; set; } = [];
}

public sealed class TotalDealsVolumeByMonthDataItem
{
    public string MonthName { get; init; } = string.Empty;

    public int MonthIndex { get; init; }

    public decimal DealsVolume { get; init; }
}