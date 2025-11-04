using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Models.Pagination;
using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Application.Brokers.GetBrokersLastDealsOverTime;

public sealed class GetBrokersLastDealsOverTimeQueryHandler
    : IQueryHandler<GetBrokersLastDealsOverTimeQuery, PagedResult<GetBrokersLastDealsOverTimeResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetBrokersLastDealsOverTimeQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<PagedResult<GetBrokersLastDealsOverTimeResponse>>> Handle(GetBrokersLastDealsOverTimeQuery request, CancellationToken cancellationToken)
    {
        var startDate = request.Request.StartDate.ToDateTime(TimeOnly.MinValue);
        var endDate = request.Request.EndDate.ToDateTime(TimeOnly.MaxValue);

        var baseQuery = _dbContext.Deals
            .Where(d => d.DateOnUtc >= startDate.ToUniversalTime() && d.DateOnUtc <= endDate.ToUniversalTime()
            && d.SellerBrokerId == request.Request.Id || d.BuyerBrokerId == request.Request.Id)
            .OrderBy(d => d.DateOnUtc)
           .AsQueryable()
           .AsNoTracking();

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var deals = await baseQuery
            .Skip((request.Request.PageNumber - 1) * request.Request.PageSize)
            .Take(request.Request.PageSize)
            .Select(d => new GetBrokersLastDealsOverTimeResponse
            {
                Id = d.Id,
                Number = d.Number.Value,
                Amount = d.Amount.Value,
                Price = d.Price.Value,
                Cost = d.Cost.Amount,
                DateOnUtc = d.DateOnUtc,
                Status = d.Status
            })
            .ToListAsync(cancellationToken);

        var result = new PagedResult<GetBrokersLastDealsOverTimeResponse>
        {
            Items = [],
            TotalCount = 0,
            FilteredCount = 0,
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize
        };

        if (deals.Count > 0)
        {
            result.Items = deals;
            result.TotalCount = totalCount;
            result.FilteredCount = totalCount;
        }

        return result;
    }
}
