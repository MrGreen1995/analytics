namespace UzEx.Analytics.Application.Deals.GetDealsCountTrendOverTime;

public sealed class GetDealsCountTrendOverTimeResponse
{
    public decimal PercentageChange { get; init; }

    public string FormattedChange { get; init; } = string.Empty;

    public string Icon { get; set; } = string.Empty;

    public string CssClass { get; set; } = string.Empty;
}
