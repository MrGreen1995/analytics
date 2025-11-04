using Microsoft.EntityFrameworkCore;
using System.Globalization;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Contracts;

namespace UzEx.Analytics.Application.Contracts.GetTotalContractsCountByTradeTypeOverTime;

public sealed class GetTotalContractsCountByTradeTypeOverTimeQueryHandler :
    IQueryHandler<GetTotalContractsCountByTradeTypeOverTimeQuery, List<GetTotalContractsCountByTradeTypeOverTimeResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetTotalContractsCountByTradeTypeOverTimeQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<List<GetTotalContractsCountByTradeTypeOverTimeResponse>>> Handle(GetTotalContractsCountByTradeTypeOverTimeQuery request, CancellationToken cancellationToken)
    {
        var startDate = request.Request.StartDate.ToDateTime(TimeOnly.MinValue);
        var endDate = request.Request.EndDate.ToDateTime(TimeOnly.MaxValue);

        var aggregatedData = await _dbContext.Contracts
            .Where(c => c.CreatedOnUtc >= startDate.ToUniversalTime() && c.CreatedOnUtc <= endDate.ToUniversalTime()
            && c.TradeType != ContractTradeType.Undefined)
            .GroupBy(c => new
            {
                ContractTradeType = c.TradeType,
                Year = c.CreatedOnUtc.Year,
                Month = c.CreatedOnUtc.Month
            })
            .Select(g => new
            {
                g.Key.ContractTradeType,
                g.Key.Year,
                g.Key.Month,
                TotalContractsCount = g.Count()
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var result = aggregatedData
            .GroupBy(r => new { r.ContractTradeType, r.Year })
            .Select(g => new GetTotalContractsCountByTradeTypeOverTimeResponse
            {
                TradeType = Enum.GetName(typeof(ContractTradeType), g.Key.ContractTradeType)!,
                Year = g.Key.Year,
                Data = g.OrderBy(x => x.Month)
                .Select(x => new ContractsCountOfDirectionByMonthDataItem
                {
                    MonthIndex = x.Month,
                    MonthName = new DateTime(x.Year, x.Month, 1).ToString("MMMM", CultureInfo.InvariantCulture),
                    OrdersCount = x.TotalContractsCount
                })
                .ToList()
            })
            .OrderBy(x => x.TradeType)
            .ThenBy(x => x.Year)
            .ToList();

        return result;
    }
}
