using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Models.Pagination;
using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Application.Brokers.GetBrokerClientsOverTime;

public sealed class GetBrokerClientsOverTimeQueryHandler
    : IQueryHandler<GetBrokerClientsOverTimeQuery, PagedResult<GetBrokerClientsOverTimeResponse>>
{

    private readonly IApplicationDbContext _dbContext;

    public GetBrokerClientsOverTimeQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<PagedResult<GetBrokerClientsOverTimeResponse>>> Handle(GetBrokerClientsOverTimeQuery request, CancellationToken cancellationToken)
    {
        var startDate = request.Request.StartDate.ToDateTime(TimeOnly.MinValue);
        var endDate = request.Request.EndDate.ToDateTime(TimeOnly.MaxValue);

        var query = _dbContext.Orders
            .Where(o => o.ReceiveDate >= startDate &&
            o.ReceiveDate <= endDate &&
            o.BrokerId == request.Request.BrokerId);

        var totalClientsCount = await query.CountAsync(cancellationToken);

        var clients = await query
            .GroupBy(o => new
            {
                ClientId = o.ClientId
            })
            .Skip((request.Request.PageNumber - 1) * request.Request.PageSize)
            .Take(request.Request.PageSize)
            .Select(g => new GetBrokerClientsOverTimeResponse
            {
                Id = g.Key.ClientId,
                Type = g.FirstOrDefault()!.Client.Type,
                RegNumber = g.FirstOrDefault()!.Client.RegNumber.Value,
                Name = g.FirstOrDefault()!.Client.Name.Value,
                Country = g.FirstOrDefault()!.Client.Country.Value,
                Region = g.FirstOrDefault()!.Client.Region.Value,
                District = g.FirstOrDefault()!.Client.District.Value,
                Address = g.FirstOrDefault()!.Client.Address.Value,
                OrdersCount = g.Count()
            })
            .AsNoTracking()
            .OrderByDescending(x => x.OrdersCount)
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
            result.TotalCount = totalClientsCount;
            result.FilteredCount = totalClientsCount;
        }

        return result;
    }
}
