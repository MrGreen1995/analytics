using Microsoft.EntityFrameworkCore;
using System.Globalization;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Deals;

namespace UzEx.Analytics.Application.Deals.GetTotalDealsCountOverTime;

public sealed class GetTotalDealsCountOverTimeQueryHandler
    : IQueryHandler<GetTotalDealsCountOverTimeQuery, List<GetTotalDealsCountOverTimeResponse>>
{

    private readonly IApplicationDbContext _dbContext;

    public GetTotalDealsCountOverTimeQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<List<GetTotalDealsCountOverTimeResponse>>> Handle(GetTotalDealsCountOverTimeQuery request, CancellationToken cancellationToken)
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
                Year = d.DateOnUtc.Year,
                Month = d.DateOnUtc.Month,
            })
            .Select(g => new
            {
                g.Key.Year,
                g.Key.Month,
                TotalDealsCount = g.Count(),
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var result = aggregatedData
            .GroupBy(r => new
            {
                r.Year
            })
            .Select(g => new GetTotalDealsCountOverTimeResponse
            {
                Year = g.Key.Year,
                Data = g.OrderBy(d => d.Month)
                .Select(x => new TotalDealsCountByMonthDataItem
                {
                    MonthIndex = x.Month,
                    MonthName = new DateTime(x.Year, x.Month, 1).ToString("MMMM", CultureInfo.InvariantCulture),
                    DealsCount = x.TotalDealsCount
                })
                .ToList()
            })
            .OrderBy(r => r.Year)
            .ToList();

        return result;
    }
}
