namespace UzEx.Analytics.Application.Deals.GetTotalDealSumByMonth;

public sealed class GetTotalDealSumByMonthResponse
{
    public int Month { get; init; }
    
    public decimal Cost { get; init; }

    public decimal CostConverted { get; set; }
    
    public string? CostUnit { get; set; }
}