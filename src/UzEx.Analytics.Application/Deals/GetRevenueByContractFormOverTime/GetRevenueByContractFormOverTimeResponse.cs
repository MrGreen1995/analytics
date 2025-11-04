namespace UzEx.Analytics.Application.Deals.GetRevenueByContractFormOverTime;

public sealed class GetRevenueByContractFormOverTimeResponse
{
    public string ContractForm { get; init; } = string.Empty;

    public int Year { get; init; }

    public List<RevenueOfContractFormByMonthDataItem> Data { get; set; } = [];

    public double TotalDealsCount => Data.Sum(c => c.DealsCount);

    public decimal TotalDealsSum => Data.Sum(c => c.DealsSum);
}

public sealed class RevenueOfContractFormByMonthDataItem
{
    public string MonthName { get; init; } = string.Empty;

    public int MonthIndex { get; init; }

    public double DealsCount { get; init; }

    public decimal DealsSum { get; init; }
}
