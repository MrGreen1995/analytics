using UzEx.Analytics.Application.Abstractions.Messaging;

namespace UzEx.Analytics.Application.Deals.ExportDealsToExcel;

public sealed record ExportDealsToExcelQuery(ExportDealsToExcelRequest Request) : IQuery<byte[]>
{
}
