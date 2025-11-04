using Microsoft.EntityFrameworkCore;
using System.Globalization;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Contracts;
using UzEx.Analytics.Domain.Deals;

namespace UzEx.Analytics.Application.Deals.GetRevenueByPlatformOverTime;

public sealed class GetRevenueByPlatformOverTimeQueryHandler : IQueryHandler<GetRevenueByPlatformOverTimeQuery, List<GetRevenueByPlatformOverTimeResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetRevenueByPlatformOverTimeQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<List<GetRevenueByPlatformOverTimeResponse>>> Handle(GetRevenueByPlatformOverTimeQuery request, CancellationToken cancellationToken)
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
                Platform = d.Contract.Platform,
                Year = d.DateOnUtc.Year,
                Month = d.DateOnUtc.Month
            })
            .Select(g => new
            {
                g.Key.Platform,
                g.Key.Year,
                g.Key.Month,
                DealsCount = g.Count(),
                DealsSum = g.Sum(x => x.Cost.Amount)
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var result = aggregatedData
            .GroupBy(r => new
            {
                r.Platform,
                r.Year
            })
            .Select(g => new GetRevenueByPlatformOverTimeResponse
            {
                Platform = Enum.GetName(typeof(ContractPlatformType), g.Key.Platform)!,
                Year = g.Key.Year,
                Data = g.OrderBy(x => x.Month)
                .Select(x => new RevenueOfPlatformByMonthDataItem
                {
                    MonthIndex = x.Month,
                    MonthName = new DateTime(x.Year, x.Month, 1).ToString("MMMM", CultureInfo.InvariantCulture),
                    DealsCount = x.DealsCount,
                    DealsSum = x.DealsSum
                })
                .ToList()
            })
            .OrderBy(r => r.Platform)
            .ThenBy(r => r.Year)
            .ToList();

        return result;
    }
}
