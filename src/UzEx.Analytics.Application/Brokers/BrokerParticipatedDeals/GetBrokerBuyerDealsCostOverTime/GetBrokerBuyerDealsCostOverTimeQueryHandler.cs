using Microsoft.EntityFrameworkCore;
using System.Globalization;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Brokers.BrokerParticipatedDeals.Shared;
using UzEx.Analytics.Application.Extensions;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Deals;

namespace UzEx.Analytics.Application.Brokers.BrokerParticipatedDeals.GetBrokerBuyerDealsCostOverTime;

public sealed class GetBrokerBuyerDealsCostOverTimeQueryHandler
    : IQueryHandler<GetBrokerBuyerDealsCostOverTimeQuery, List<GetBrokerHandledDealsCostOverTimeResponse>>
{

    private readonly IApplicationDbContext _dbContext;

    public GetBrokerBuyerDealsCostOverTimeQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<List<GetBrokerHandledDealsCostOverTimeResponse>>> Handle(GetBrokerBuyerDealsCostOverTimeQuery request, CancellationToken cancellationToken)
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

        var aggregatedData = await _dbContext.Deals
            .Where(d =>
            d.BuyerBrokerId == request.Request.Id &&
            d.DateOnUtc >= startDate &&
            d.DateOnUtc <= endDate &&
            acceptedStatuses.Contains(d.Status))
            .GroupBy(d => new
            {
                Year = d.DateOnUtc.Year,
                Month = d.DateOnUtc.Month
            })
            .Select(g => new
            {
                g.Key.Year,
                g.Key.Month,
                DealsCount = g.Count(),
                DealsSum = g.Sum(x => x.Cost.Amount)
            })
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var allMonths = DateTimeHelper.GetMonthsInRange(startDate, endDate);

        var mergedData = allMonths
            .GroupBy(m => m.Year)
            .Select(yearGroup => new GetBrokerHandledDealsCostOverTimeResponse
            {
                BrokerId = request.Request.Id,
                ClientDirection = "Buyer Broker Deals",
                Year = yearGroup.Key,
                Data = yearGroup
                .Select(m =>
                {
                    var monthData = aggregatedData.FirstOrDefault(x => x.Year == m.Year && x.Month == m.Month);

                    return new HandledDealsByMonthDataItem
                    {
                        MonthIndex = m.Month,
                        MonthName = new DateTime(m.Year, m.Month, 1).ToString("MMMM", CultureInfo.InvariantCulture),
                        DealsCount = monthData?.DealsCount ?? 0,
                        DealsSum = monthData?.DealsSum ?? 0
                    };
                })
                .OrderBy(x => x.MonthIndex)
                .ToList()
            })
            .OrderBy(x => x.Year)
            .ToList();

        return mergedData;
    }
}
