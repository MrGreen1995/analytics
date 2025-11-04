using UzEx.Analytics.Application.Abstractions.Messaging;

namespace UzEx.Analytics.Application.Orders.ExportOrdersToExcel;

public sealed record ExportOrdersToExcelQuery(ExportOrdersToExcelRequest Request) : IQuery<byte[]>
{
}
