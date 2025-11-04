using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Models.Pagination;
using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Application.Brokers.GetMostActiveBrokersByRegionOverTime;

public sealed class GetMostActiveBrokersByRegionOverTimeQueryHandler
    : IQueryHandler<GetMostActiveBrokersByRegionOverTimeQuery, PagedResult<GetMostActiveBrokersByRegionOverTimeResponse>>
{

    private readonly IApplicationDbContext _dbContext;

    public GetMostActiveBrokersByRegionOverTimeQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<PagedResult<GetMostActiveBrokersByRegionOverTimeResponse>>> Handle(GetMostActiveBrokersByRegionOverTimeQuery request, CancellationToken cancellationToken)
    {
        var startDate = request.Request.StartDate.ToDateTime(TimeOnly.MinValue).ToUniversalTime();
        var endDate = request.Request.EndDate.ToDateTime(TimeOnly.MaxValue).ToUniversalTime();

        var activeBrokersQuery = _dbContext.Brokers
            //.Where(b => b.Region.Value == request.Request.Region.ToString()
            //&& b.SellerBrokerDeals != null || b.BuyerBrokerDeals != null)
            .AsNoTracking()
            .Select(b => new
            {
                BrokerDetails = b,

                ActivityCount =
                b.SellerBrokerDeals!.Count(d => d.DateOnUtc >= startDate && d.DateOnUtc <= endDate)
                + b.BuyerBrokerDeals!.Count(d => d.DateOnUtc >= startDate && d.DateOnUtc <= endDate),

                SellerDealsCount = b.SellerBrokerDeals!.Count(d => d.DateOnUtc >= startDate && d.DateOnUtc <= endDate),

                BuyerDealsCount = b.BuyerBrokerDeals!.Count(d => d.DateOnUtc >= startDate && d.DateOnUtc <= endDate),

                SellerDealsSum = b.SellerBrokerDeals!
                .Where(d => d.DateOnUtc >= startDate && d.DateOnUtc <= endDate)
                .Sum(d => d.Cost.Amount),

                BuyerDealsSum = b.BuyerBrokerDeals!
                .Where(d => d.DateOnUtc >= startDate && d.DateOnUtc <= endDate)
                .Sum(d => d.Cost.Amount)
            })
            .Where(x => x.ActivityCount > 0);

        var totalActiveBrokers = await activeBrokersQuery.CountAsync(cancellationToken);

        var activeBrokers = await activeBrokersQuery
            .OrderByDescending(x => x.ActivityCount)
            .Skip((request.Request.PageNumber - 1) * request.Request.PageSize)
            .Take(request.Request.PageSize)
            .Select(x => new GetMostActiveBrokersByRegionOverTimeResponse
            {
                Id = x.BrokerDetails.Id,
                BusinessKey = x.BrokerDetails.BusinessKey.Value,
                RegNumber = x.BrokerDetails.RegNumber.Value,
                Name = x.BrokerDetails.Name.Value,
                Region = x.BrokerDetails.Region.Value,
                SellerDealsCount = x.SellerDealsCount,
                BuyerDealsCount = x.BuyerDealsCount,
                SellerDealsSum = x.SellerDealsSum,
                BuyerDealsSum = x.BuyerDealsSum
            })
            .ToListAsync(cancellationToken);

        var result = new PagedResult<GetMostActiveBrokersByRegionOverTimeResponse>
        {
            Items = [],
            TotalCount = 0,
            FilteredCount = 0,
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize
        };

        if (activeBrokers.Count > 0)
        {
            result.Items = activeBrokers;
            result.TotalCount = totalActiveBrokers;
            result.FilteredCount = totalActiveBrokers;
        }

        return result;
    }
}
