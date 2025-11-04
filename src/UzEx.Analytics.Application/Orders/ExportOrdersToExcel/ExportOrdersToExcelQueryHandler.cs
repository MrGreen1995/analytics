using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Extensions;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Orders.Errors;

namespace UzEx.Analytics.Application.Orders.ExportOrdersToExcel;

public sealed class ExportOrdersToExcelQueryHandler : IQueryHandler<ExportOrdersToExcelQuery, byte[]>
{
    private readonly IApplicationDbContext _dbContext;

    public ExportOrdersToExcelQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<byte[]>> Handle(ExportOrdersToExcelQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var orders = await GetOrdersAsync(request.Request.StartDate, request.Request.EndDate, cancellationToken);

            var result = await ExcelExporter.ExportToExcel(orders, "Orders");

            return result;
        }
        catch (Exception)
        {
            return Result.Failure<byte[]>(OrderErrors.NotFound);
        }
    }

    private async Task<List<ExportOrdersToExcelResponse>> GetOrdersAsync(DateOnly start,
            DateOnly end,
            CancellationToken cancellationToken)
    {
        var startDate = start.ToDateTime(TimeOnly.MinValue);
        var endDate = end.ToDateTime(TimeOnly.MaxValue);

        var orders = await _dbContext.Orders
            .Where(o => o.ReceiveDate >= startDate.ToUniversalTime()
                          && o.ReceiveDate <= endDate.ToUniversalTime())
            .Select(order => new ExportOrdersToExcelResponse
            {
                Direction = order.Direction.ToString(),
                Amount = order.Amount.Value,
                Price = order.Price.Value,
                ReceiveDate = order.ReceiveDate,
                Status = order.Status.ToString(),
                BrokerName = order.Broker.Name.Value,
                BrokerNumber = order.Broker.Number.Value,
                ClientTin = order.Client.RegNumber.Value,
                ClientName = order.Client.Name.Value,
                ContractNumber = order.Contract.Number.Value,
                ContractName = order.Contract.Name.Value,
                ContractLot = order.Contract.Lot.Value,
                ContractUnit = order.Contract.Unit.Value,
                ContractCurrency = order.Contract.Currency.Value,
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return orders;
    }
}
