namespace UzEx.Analytics.Application.Orders.ExportOrdersToExcel;

public sealed class ExportOrdersToExcelRequest
{
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}
