using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Contracts;
using UzEx.Analytics.Domain.Deals;

namespace UzEx.Analytics.Application.Deals.GetRevenueByTradeType;

public sealed class GetRevenueByTradeTypeQueryHandler : IQueryHandler<GetRevenueByTradeTypeQuery, List<GetRevenueByTradeTypeResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetRevenueByTradeTypeQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<List<GetRevenueByTradeTypeResponse>>> Handle(GetRevenueByTradeTypeQuery request, CancellationToken cancellationToken)
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
            .GroupBy(d => d.Contract.TradeType)
            .Select(g => new
            {
                TradeTypeEnum = g.Key,
                Value = g.Sum(d => d.Cost.Amount)
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var result = aggregatedData
            .Select(x => new GetRevenueByTradeTypeResponse()
            {
                TradeType = Enum.GetName(typeof(ContractTradeType), x.TradeTypeEnum)!,
                Value = x.Value
            })
            .OrderBy(r => r.Value)
            .ToList();

        return result;
    }
}
