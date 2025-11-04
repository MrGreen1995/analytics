using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Application.Deals.GetDealsCostOverTime;

public sealed class GetDealsCostOverTimeQueryHandler
    : IQueryHandler<GetDealsCostOverTimeQuery, GetDealsCostOverTimeResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public GetDealsCostOverTimeQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<GetDealsCostOverTimeResponse>> Handle(GetDealsCostOverTimeQuery request, CancellationToken cancellationToken)
    {
        var startDate = request.Request.StartDate.ToDateTime(TimeOnly.MinValue);
        var endDate = request.Request.EndDate.ToDateTime(TimeOnly.MaxValue);

        var sum = await _dbContext.Deals
            .Where(d => d.DateOnUtc >= startDate.ToUniversalTime() && d.DateOnUtc <= endDate.ToUniversalTime())
            .SumAsync(d => d.Cost.Amount, cancellationToken);

        var result = new GetDealsCostOverTimeResponse
        {
            Sum = sum
        };

        return result;
    }
}
