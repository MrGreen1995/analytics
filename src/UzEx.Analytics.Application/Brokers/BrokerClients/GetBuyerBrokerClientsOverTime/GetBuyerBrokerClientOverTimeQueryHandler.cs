using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Brokers.BrokerClients.Shared;
using UzEx.Analytics.Application.Models.Pagination;
using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Application.Brokers.BrokerClients.GetBuyerBrokerClientsOverTime;

public sealed class GetBuyerBrokerClientOverTimeQueryHandler
    : IQueryHandler<GetBuyerBrokerClientOverTimeQuery, PagedResult<GetBrokerClientsOverTimeResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetBuyerBrokerClientOverTimeQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<PagedResult<GetBrokerClientsOverTimeResponse>>> Handle(GetBuyerBrokerClientOverTimeQuery request, CancellationToken cancellationToken)
    {
        var startDate = request.Request.StartDate.ToDateTime(TimeOnly.MinValue).ToUniversalTime();
        var endDate = request.Request.EndDate.ToDateTime(TimeOnly.MaxValue).ToUniversalTime();

        var query = _dbContext.Deals
            .Where(d =>
            d.DateOnUtc >= startDate &&
            d.DateOnUtc <= endDate &&
            d.BuyerBrokerId == request.Request.Id)
            .Include(d => d.BuyerClient);

        var totalCount = await query.CountAsync(cancellationToken);

        var clients = await query
            .GroupBy(c => new
            {
                c.BuyerClientId
            })
            .Skip((request.Request.PageNumber - 1) * request.Request.PageSize)
            .Take(request.Request.PageSize)
            .Select(g => new GetBrokerClientsOverTimeResponse
            {
                Id = g.Key.BuyerClientId,
                Type = g.FirstOrDefault()!.BuyerClient!.Type,
                RegNumber = g.FirstOrDefault()!.BuyerClient!.RegNumber.Value,
                Name = g.FirstOrDefault()!.BuyerClient!.Name.Value,
                Country = g.FirstOrDefault()!.BuyerClient!.Country.Value,
                Region = g.FirstOrDefault()!.BuyerClient!.Region.Value,
                District = g.FirstOrDefault()!.BuyerClient!.District.Value,
                Address = g.FirstOrDefault()!.BuyerClient!.Address.Value,
                DealsCount = g.Count(),
                DealsSum = g.Sum(d => d.Cost.Amount)
            })
            .AsNoTracking()
            .OrderByDescending(d => d.DealsCount)
            .ToListAsync(cancellationToken);

        var result = new PagedResult<GetBrokerClientsOverTimeResponse>
        {
            Items = [],
            FilteredCount = 0,
            TotalCount = 0,
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize
        };

        if (clients.Count > 0)
        {
            result.Items = clients;
            result.TotalCount = totalCount;
            result.FilteredCount = totalCount;
        }

        return result;
    }
}
