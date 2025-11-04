using Microsoft.EntityFrameworkCore;
using System.Globalization;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Deals;

namespace UzEx.Analytics.Application.Deals.GetTotalDealsSumOverTime;

public sealed class GetTotalDealsSumOverTimeQueryHandler
    : IQueryHandler<GetTotalDealsSumOverTimeQuery, List<GetTotalDealsSumOverTimeResponse>>
{

    private readonly IApplicationDbContext _dbContext;

    public GetTotalDealsSumOverTimeQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<List<GetTotalDealsSumOverTimeResponse>>> Handle(GetTotalDealsSumOverTimeQuery request, CancellationToken cancellationToken)
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
                d.DateOnUtc.Year,
                d.DateOnUtc.Month
            })
            .Select(g => new
            {
                g.Key.Year,
                g.Key.Month,
                DealsSum = g.Sum(x => x.Cost.Amount)
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var result = aggregatedData
            .GroupBy(r => new
            {
                r.Year
            })
            .Select(g => new GetTotalDealsSumOverTimeResponse
            {
                Year = g.Key.Year,
                Data = g.OrderBy(x => x.Month)
                .Select(x => new TotalDealsSumByMonthDataItem
                {
                    MonthIndex = x.Month,
                    MonthName = new DateTime(x.Year, x.Month, 1).ToString("MMMM", CultureInfo.InvariantCulture),
                    DealsSum = x.DealsSum
                })
                .ToList()
            })
            .OrderBy(r => r.Year)
            .ToList();

        return result;
    }
}
