using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Deals;

namespace UzEx.Analytics.Application.Brokers.GetTotalBrokersCountByRegionsOverTime;

public sealed class GetTotalBrokersCountByRegionsOverTimeQueryHandler
    : IQueryHandler<GetTotalBrokersCountByRegionsOverTimeQuery, List<GetTotalBrokersCountByRegionsOverTimeResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetTotalBrokersCountByRegionsOverTimeQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<List<GetTotalBrokersCountByRegionsOverTimeResponse>>> Handle(GetTotalBrokersCountByRegionsOverTimeQuery request, CancellationToken cancellationToken)
    {
        var startDate = request.Request.StartDate.ToDateTime(TimeOnly.MinValue).ToUniversalTime();
        var endDate = request.Request.EndDate.ToDateTime(TimeOnly.MaxValue).ToUniversalTime();

        var acceptedStatuses = new List<DealStatusType>
        {
            DealStatusType.WaitingDelivery,
            DealStatusType.NotDelivered,
            DealStatusType.Completed,
            DealStatusType.PartialCompleted,
            DealStatusType.Concluded
        };

        var result = await _dbContext.Brokers
            .Include(b => b.BuyerBrokerDeals)
            .Include(b => b.SellerBrokerDeals)
            .GroupBy(b => new
            {
                Region = b.Region,
            })
            .Select(g => new GetTotalBrokersCountByRegionsOverTimeResponse
            {
                RegionIndex = g.Key.Region.Value,

                BrokersCount = g.Count(),

                DealsCount =
                g.Sum(x =>
                x.BuyerBrokerDeals!
                .Where(d => d.DateOnUtc >= startDate &&
                d.DateOnUtc <= endDate &&
                acceptedStatuses.Contains(d.Status))!
                .Count())
                +
                g.Sum(x =>
                x.SellerBrokerDeals!
                .Where(d => d.DateOnUtc >= startDate &&
                d.DateOnUtc <= endDate &&
                acceptedStatuses.Contains(d.Status))!
                .Count()),

                DealsSum =
                g.Sum(x =>
                x.BuyerBrokerDeals!
                .Where(d => d.DateOnUtc >= startDate &&
                d.DateOnUtc <= endDate &&
                acceptedStatuses.Contains(d.Status))!
                .Sum(d => d.Cost.Amount))
                +
                g.Sum(x =>
                x.SellerBrokerDeals!
                .Where(d => d.DateOnUtc >= startDate &&
                d.DateOnUtc <= endDate &&
                acceptedStatuses.Contains(d.Status))!
                .Sum(d => d.Cost.Amount))
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return result;
    }
}
