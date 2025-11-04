using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Contracts;
using UzEx.Analytics.Domain.Orders;

namespace UzEx.Analytics.Application.Orders.GetTotalOrdersCountByDirection;

public sealed class GetTotalOrdersCountByDirectionQueryHandler :
    IQueryHandler<GetTotalOrdersCountByDirectionQuery, List<GetTotalOrdersCountByDirectionResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetTotalOrdersCountByDirectionQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<List<GetTotalOrdersCountByDirectionResponse>>> Handle(GetTotalOrdersCountByDirectionQuery request, CancellationToken cancellationToken)
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
            .GroupBy(o => o.Direction)
            .Select(g => new
            {
                OrderDirectionTypeEnum = g.Key,
                Count = g.Count(),
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var result = aggregatedData
            .Select(r => new GetTotalOrdersCountByDirectionResponse
            {
                DirectionType = Enum.GetName(typeof(OrderDirectionType), r.OrderDirectionTypeEnum)!,
                Count = r.Count
            })
            .OrderBy(r => r.Count)
            .ToList();

        return result;

    }
}
