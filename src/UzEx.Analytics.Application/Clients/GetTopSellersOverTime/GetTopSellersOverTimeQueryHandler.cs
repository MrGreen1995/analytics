using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Models.Pagination;
using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Application.Clients.GetTopSellersOverTime;

public sealed class GetTopSellersOverTimeQueryHandler
    : IQueryHandler<GetTopSellersOverTimeQuery, PagedResult<GetTopSellersOverTimeResponse>>
{

    private readonly IApplicationDbContext _dbContext;

    public GetTopSellersOverTimeQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<PagedResult<GetTopSellersOverTimeResponse>>> Handle(GetTopSellersOverTimeQuery request, CancellationToken cancellationToken)
    {
        var startDate = request.Request.StartDate.ToDateTime(TimeOnly.MinValue);
        var endDate = request.Request.EndDate.ToDateTime(TimeOnly.MaxValue);

        var topSellersQuery = _dbContext.Clients
            .Where(c => c.SellerDeals != null)
            .Select(c => new
            {
                ClientDetails = c,

                DealsCost = c.SellerDeals!
                .Where(d => d.DateOnUtc >= startDate.ToUniversalTime() && d.DateOnUtc <= endDate.ToUniversalTime())
                .Sum(d => d.Cost.Amount),

                ActivityCount = c.SellerDeals!
                .Count(d => d.DateOnUtc >= startDate.ToUniversalTime() && d.DateOnUtc <= endDate.ToUniversalTime()),
            })
            .Where(x => x.ActivityCount > 0 || x.DealsCost > 0)
            .AsNoTracking();

        var topSellersCount = await topSellersQuery.CountAsync(cancellationToken);

        var topClients = await topSellersQuery
            .OrderBy(x => x.ActivityCount)
            .Skip((request.Request.PageNumber - 1) * request.Request.PageSize)
            .Take(request.Request.PageSize)
            .Select(x => new GetTopSellersOverTimeResponse
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

        var result = new PagedResult<GetTopSellersOverTimeResponse>
        {
            Items = [],
            TotalCount = 0,
            FilteredCount = 0,
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize
        };

        if (topClients.Count > 0)
        {
            result.Items = topClients;
            result.TotalCount = topSellersCount;
            result.FilteredCount = topSellersCount;
        }

        return result;
    }
}
