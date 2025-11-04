namespace UzEx.Analytics.Application.Brokers.BrokerParticipatedDeals.Shared;

public sealed class GetBrokerHandledDealsCostOverTimeResponse
{
    public Guid BrokerId { get; set; }

    public string ClientDirection { get; init; } = string.Empty;

    public int Year { get; init; }

    public List<HandledDealsByMonthDataItem> Data { get; set; } = [];

    public decimal TotalDealsSum => Data.Sum(x => x.DealsSum);

    public decimal TotalDealsCount => Data.Sum(x => x.DealsCount);
}

public sealed class HandledDealsByMonthDataItem
{
    public string MonthName { get; init; } = string.Empty;

    public int MonthIndex { get; init; }

    public decimal DealsSum { get; init; }

    public decimal DealsCount { get; init; }
}
