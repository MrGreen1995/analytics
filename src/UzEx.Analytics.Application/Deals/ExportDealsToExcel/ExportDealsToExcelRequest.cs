namespace UzEx.Analytics.Application.Deals.ExportDealsToExcel;

public sealed class ExportDealsToExcelRequest
{
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}
