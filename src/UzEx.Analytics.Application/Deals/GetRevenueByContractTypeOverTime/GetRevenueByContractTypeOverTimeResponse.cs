namespace UzEx.Analytics.Application.Deals.GetRevenueByContractTypeOverTime;

public sealed class GetRevenueByContractTypeOverTimeResponse
{
    public string ContractType { get; init; } = string.Empty;

    public int Year { get; init; }

    public List<RevenueOfContractTypeByMonthDataItem> Data { get; set; } = [];

    public double TotalDealsCount => Data.Sum(c => c.DealsCount);

    public decimal TotalDealsSum => Data.Sum(c => c.DealsSum);
}

public sealed class RevenueOfContractTypeByMonthDataItem
{
    public string MonthName { get; init; } = string.Empty;

    public int MonthIndex { get; init; }

    public double DealsCount { get; init; }

    public decimal DealsSum { get; init; }
}