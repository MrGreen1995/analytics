using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Models.Pagination;
using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Application.Clients.GetTopBuyersOverTime;

public sealed class GetTopBuyersOverTimeQueryHandler :
     IQueryHandler<GetTopBuyersOverTimeQuery, PagedResult<GetTopBuyersOverTimeResponse>>
{

    private readonly IApplicationDbContext _dbContext;

    public async Task<Result<PagedResult<GetTopBuyersOverTimeResponse>>> Handle(GetTopBuyersOverTimeQuery request, CancellationToken cancellationToken)
    {
        var startDate = request.Request.StartDate.ToDateTime(TimeOnly.MinValue);
        var endDate = request.Request.EndDate.ToDateTime(TimeOnly.MaxValue);


        var topBuyersQuery = _dbContext.Clients
            .Where(c => c.BuyerDeals != null)
            .Select(c => new
            {
                ClientDetails = c,

                DealsCost = c.BuyerDeals!
                 .Where(d => d.DateOnUtc >= startDate.ToUniversalTime() && d.DateOnUtc <= endDate.ToUniversalTime())
                .Sum(d => d.Cost.Amount),

                ActivityCount = c.BuyerDeals!
                .Count(d => d.DateOnUtc >= startDate.ToUniversalTime() && d.DateOnUtc <= endDate.ToUniversalTime()),
            })
            .Where(x => x.ActivityCount > 0 || x.DealsCost > 0)
            .AsNoTracking();

        var topBuyersCount = await topBuyersQuery.CountAsync(cancellationToken);

        var topBuyers = await topBuyersQuery
            .OrderBy(x => x.ActivityCount)
            .Skip((request.Request.PageNumber - 1) * request.Request.PageSize)
            .Take(request.Request.PageSize)
            .Select(x => new GetTopBuyersOverTimeResponse
            {
                Id = x.ClientDetails.Id,
                Name = x.ClientDetails.Name.Value,
                RegNumber = x.ClientDetails.RegNumber.Value,
                Type = x.ClientDetails.Type,
                Country = x.ClientDetails.Country.Value,
                Region = x.ClientDetails.Region.Value,
                TotalDealsCount = x.ActivityCount,
                TotalDealsCost = x.DealsCost
            })
            .ToListAsync(cancellationToken);

        var result = new PagedResult<GetTopBuyersOverTimeResponse>
        {
            Items = [],
            TotalCount = 0,
            FilteredCount = 0,
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize
        };

        if (topBuyers.Count > 0)
        {
            result.Items = topBuyers;
            result.TotalCount = topBuyersCount;
            result.FilteredCount = topBuyersCount;
        }

        return result;
    }
}
