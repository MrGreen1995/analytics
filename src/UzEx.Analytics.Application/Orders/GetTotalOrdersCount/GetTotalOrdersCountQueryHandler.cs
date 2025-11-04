using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Orders;

namespace UzEx.Analytics.Application.Orders.GetTotalOrdersCount;

public sealed class GetTotalOrdersCountQueryHandler : IQueryHandler<GetTotalOrdersCountQuery, GetTotalOrdersCountResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public GetTotalOrdersCountQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<GetTotalOrdersCountResponse>> Handle(GetTotalOrdersCountQuery request, CancellationToken cancellationToken)
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

        var count = await _dbContext.Orders
            .Where(o => o.ReceiveDate >= startDate.ToUniversalTime() && o.ReceiveDate <= endDate.ToUniversalTime()
            && acceptedStatuses.Contains(o.Status))
            .CountAsync(cancellationToken);

        var result = new GetTotalOrdersCountResponse()
        {
            Count = count
        };

        return result;
    }
}
