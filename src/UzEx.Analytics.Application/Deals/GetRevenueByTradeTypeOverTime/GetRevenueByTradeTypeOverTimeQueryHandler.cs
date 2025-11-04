using Microsoft.EntityFrameworkCore;
using System.Globalization;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Contracts;
using UzEx.Analytics.Domain.Deals;

namespace UzEx.Analytics.Application.Deals.GetRevenueByTradeTypeOverTime;

public sealed class GetRevenueByTradeTypeOverTimeQueryHandler :
    IQueryHandler<GetRevenueByTradeTypeOverTimeQuery, List<GetRevenueByTradeTypeOverTimeResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetRevenueByTradeTypeOverTimeQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<List<GetRevenueByTradeTypeOverTimeResponse>>> Handle(GetRevenueByTradeTypeOverTimeQuery request, CancellationToken cancellationToken)
    {
        var startDate = request.Request.StartDate.ToDateTime(TimeOnly.MinValue);
        var endDate = request.Request.EndDate.ToDateTime(TimeOnly.MaxValue);

        var acceptedStatuses = new List<DealStatusType>
        {
            DealStatusType.WaitingDelivery,
            DealStatusType.NotDelivered,
            DealStatusType.Completed,
            DealStatusType.PartialCompleted,
            DealStatusType.Concluded
        };

        var aggregatedData = await _dbContext.Deals
             .Where(d => d.DateOnUtc >= startDate.ToUniversalTime() && d.DateOnUtc <= endDate.ToUniversalTime()
              && acceptedStatuses.Contains(d.Status))
             .GroupBy(d => new
             {
                 TradeType = d.Contract!.TradeType,
                 Year = d.DateOnUtc.Year,
                 Month = d.DateOnUtc.Month
             })
             .Select(g => new
             {
                 g.Key.TradeType,
                 g.Key.Year,
                 g.Key.Month,
                 DealsCount = g.Count(),
                 DealsSum = g.Sum(d => d.Cost.Amount)
             })
             .AsNoTracking()
             .ToListAsync(cancellationToken);

        var result = aggregatedData
            .GroupBy(r => new { r.TradeType, r.Year })
            .Select(g => new GetRevenueByTradeTypeOverTimeResponse
            {
                TradeType = Enum.GetName(typeof(ContractTradeType), g.Key.TradeType)!,
                Year = g.Key.Year,
                Data = g.OrderBy(x => x.Month)
                .Select(x => new RevenueOfTradeTypeByMonthDataItem()
                {
                    MonthIndex = x.Month,
                    MonthName = new DateTime(x.Year, x.Month, 1).ToString("MMMM", CultureInfo.InvariantCulture),
                    DealsCount = (double)x.DealsCount,
                    DealsSum = x.DealsSum
                })
                .ToList()
            })
            .OrderBy(r => r.TradeType)
            .ThenBy(r => r.Year)
            .ToList();

        return result;
    }
}
