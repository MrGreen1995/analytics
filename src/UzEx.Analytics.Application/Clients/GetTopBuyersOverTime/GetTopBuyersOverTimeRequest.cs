namespace UzEx.Analytics.Application.Clients.GetTopBuyersOverTime;

public sealed class GetTopBuyersOverTimeRequest
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;

    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}
