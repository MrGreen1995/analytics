using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Extensions;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Deals.Errors;

namespace UzEx.Analytics.Application.Deals.ExportDealsToExcel;

public sealed class ExportDealsToExcelQueryHandler : IQueryHandler<ExportDealsToExcelQuery, byte[]>
{
    private readonly IApplicationDbContext _dbContext;

    public ExportDealsToExcelQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<byte[]>> Handle(ExportDealsToExcelQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var deals = await GetDealsAsync(request.Request.StartDate, request.Request.EndDate, cancellationToken);

            var result = await ExcelExporter.ExportToExcel(deals, "Deals");

            return result;
        }
        catch (Exception)
        {
            return Result.Failure<byte[]>(DealErrors.NotFound);
        }
    }

    private async Task<List<ExportDealsToExcelResponse>> GetDealsAsync(
        DateOnly start,
        DateOnly end,
        CancellationToken cancellationToken)
    {
        var startDate = start.ToDateTime(TimeOnly.MinValue);
        var endDate = end.ToDateTime(TimeOnly.MaxValue);
        
        var deals = await _dbContext.Deals
            .Where(d => d.DateOnUtc >= startDate.ToUniversalTime() 
                          && d.DateOnUtc <= endDate.ToUniversalTime())
            .Select(deal => new ExportDealsToExcelResponse
            {
                CreatedOnUtc = deal.CreatedOnUtc,
                DateOnUtc = startDate,
                Number = deal.Number.Value,
                ContractName = deal.Contract.Name.Value,
                ContractNumber = deal.Contract.Number.Value,
                ContractLot = deal.Contract.Lot.Value,
                ContractUnit = deal.Contract.Unit.Value,
                ContractCurrency = deal.Contract.Currency.Value,
                SellerClientTin = deal.SellerClient.RegNumber.Value,
                SellerClientName = deal.SellerClient.Name.Value,
                BuyerClientTin = deal.BuyerClient.RegNumber.Value,
                BuyerClientName = deal.BuyerClient.Name.Value,
                SellerBrokerName = deal.SellerBroker.Name.Value,
                SellerBrokerNumber = deal.SellerBroker.Number.Value,
                BuyerBrokerName = deal.BuyerBroker.Name.Value,
                BuyerBrokerNumber = deal.BuyerBroker.Number.Value,
                SessionType = deal.SessionType.ToString(),
                Amount = deal.Amount.Value,
                Price = deal.Price.Value,
                Cost = deal.Cost.Amount,
                Status = deal.Status.ToString(),
                PaymentDays = deal.PaymentDays.Value,
                DeliveryDays = deal.DeliveryDays.Value,
                PaymentDate = deal.PaymentDate,
                CloseDate = deal.CloseDate,
                SellerTradeCommission = deal.SellerTradeCommission.Amount,
                SellerClearingCommission = deal.SellerClearingCommission.Amount,
                SellerPledge = deal.SellerPledge.Amount,
                BuyerTradeCommission = deal.BuyerTradeCommission.Amount,
                BuyerClearingCommission = deal.BuyerClearingCommission.Amount,
                BuyerPledge = deal.BuyerPledge.Amount
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return deals;
    }
}
