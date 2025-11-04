using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Models.Pagination;
using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Application.Brokers.GetBrokersLastOrdersOverTime;

public sealed class GetBrokersLastOrdersOverTimeQueryHandler
    : IQueryHandler<GetBrokersLastOrdersOverTimeQuery, PagedResult<GetBrokersLastOrdersOverTimeResponse>>
{

    private readonly IApplicationDbContext _dbContext;

    public GetBrokersLastOrdersOverTimeQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<PagedResult<GetBrokersLastOrdersOverTimeResponse>>> Handle(GetBrokersLastOrdersOverTimeQuery request, CancellationToken cancellationToken)
    {
        var startDate = request.Request.StartDate.ToDateTime(TimeOnly.MinValue);
        var endDate = request.Request.EndDate.ToDateTime(TimeOnly.MaxValue);

        var baseQuery = _dbContext.Orders
            .Where(o => o.ReceiveDate >= startDate.ToUniversalTime() && o.ReceiveDate <= endDate.ToUniversalTime()
            && o.BrokerId == request.Request.Id)
             .OrderBy(o => o.ReceiveDate)
             .AsNoTracking();

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var orders = await baseQuery
            .Skip((request.Request.PageNumber - 1) * request.Request.PageSize)
            .Take(request.Request.PageSize)
            .Select(o => new GetBrokersLastOrdersOverTimeResponse
            {
                Id = o.Id,
                Direction = o.Direction.ToString(),
                Amount = o.Amount.Value,
                Price = o.Price.Value,
                ClientTin = o.Client.RegNumber.Value,
                ClientName = o.Client.Name.Value,
                ContractName = o.Contract.Name.Value,
                ContractNumber = o.Contract.Number.Value,
                ContractLot = o.Contract.Lot.Value,
                ContractCurrency = o.Contract.Currency.Value,
                ContractUnit = o.Contract.Unit.Value
            })
            .ToListAsync(cancellationToken);

        var result = new PagedResult<GetBrokersLastOrdersOverTimeResponse>
        {
            Items = [],
            TotalCount = 0,
            FilteredCount = 0,
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize
        };

        if (orders.Count > 0)
        {
            result.Items = orders;
            result.TotalCount = totalCount;
            result.FilteredCount = totalCount;
        }

        return result;
    }
}
