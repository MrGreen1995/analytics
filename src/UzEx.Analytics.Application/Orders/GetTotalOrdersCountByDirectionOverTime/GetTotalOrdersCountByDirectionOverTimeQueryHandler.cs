using Microsoft.EntityFrameworkCore;
using System.Globalization;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Orders;

namespace UzEx.Analytics.Application.Orders.GetTotalOrdersCountByDirectionOverTime;

public sealed class GetTotalOrdersCountByDirectionOverTimeQueryHandler :
    IQueryHandler<GetTotalOrdersCountByDirectionOverTimeQuery, List<GetTotalOrdersCountByDirectionOverTimeResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetTotalOrdersCountByDirectionOverTimeQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<List<GetTotalOrdersCountByDirectionOverTimeResponse>>> Handle(GetTotalOrdersCountByDirectionOverTimeQuery request, CancellationToken cancellationToken)
    {
        var acceptedStatuses = new List<OrderStatus>
        {
            OrderStatus.Accepted,
            OrderStatus.Satisfied,
            OrderStatus.PartialSatisfied,
            OrderStatus.Updated
        };

        var startDate = request.Request.StartDate.ToDateTime(TimeOnly.MinValue);
        var endDate = request.Request.EndDate.ToDateTime(TimeOnly.MaxValue);

        var aggregatedData = await _dbContext.Orders
            .Where(o => o.ReceiveDate >= startDate.ToUniversalTime() && o.ReceiveDate <= endDate.ToUniversalTime()
            && acceptedStatuses.Contains(o.Status) && o.Direction != OrderDirectionType.Undefined)
            .GroupBy(o => new
            {
                OrderDirection = o.Direction,
                Year = o.ReceiveDate.Year,
                Month = o.ReceiveDate.Month
            })
            .Select(g => new
            {
                g.Key.OrderDirection,
                g.Key.Year,
                g.Key.Month,
                TotalOrdersCount = g.Count()
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var result = aggregatedData
            .GroupBy(r => new { r.OrderDirection, r.Year })
            .Select(g => new GetTotalOrdersCountByDirectionOverTimeResponse
            {
                DirectionType = Enum.GetName(typeof(OrderDirectionType), g.Key.OrderDirection)!,
                Year = g.Key.Year,
                Data = g.OrderBy(x => x.Month)
                .Select(x => new OrdersCountOfDirectionByMonthDataItem
                {
                    MonthIndex = x.Month,
                    MonthName = new DateTime(x.Year, x.Month, 1).ToString("MMMM", CultureInfo.InvariantCulture),
                    OrdersCount = x.TotalOrdersCount
                })
                .ToList()
            })
            .OrderBy(r => r.DirectionType)
            .ThenBy(r => r.Year)
            .ToList();

        return result;
    }
}
