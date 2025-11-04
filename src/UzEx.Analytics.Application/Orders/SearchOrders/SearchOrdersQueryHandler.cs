using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Extensions;
using UzEx.Analytics.Application.Models.Pagination;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Orders;
using UzEx.Analytics.Domain.Orders.Errors;

namespace UzEx.Analytics.Application.Orders.SearchOrders;

public sealed class SearchOrdersQueryHandler : IQueryHandler<SearchOrdersQuery, PagedResult<SearchOrdersResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public SearchOrdersQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<PagedResult<SearchOrdersResponse>>> Handle(SearchOrdersQuery request, CancellationToken cancellationToken)
    {
        var baseQuery = _dbContext.Orders
            .AsQueryable()
            .AsNoTracking();

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        IQueryable<Order> filteredQuery = baseQuery;

        // Filter
        //filteredQuery = ApplyFilters(filteredQuery, request.Request);

        var filteredCount = await filteredQuery.CountAsync(cancellationToken);

        // Sorting
        filteredQuery = filteredQuery.ApplySorting(request.Request.SortCriteria, "ReceiveDate");

        // paging & projection
        var orders = await filteredQuery
            .Skip((request.Request.PageNumber - 1) * request.Request.PageSize)
            .Take(request.Request.PageSize)
            .Select(order => new SearchOrdersResponse
            {
                Id = order.Id,
                Direction = order.Direction.ToString().ToUpper().Trim(),
                Amount = order.Amount.Value,
                Price = order.Price.Value,
                BusinessKey = order.BusinessKey.Value,
                ClientTin = order.Client.RegNumber.Value,
                ClientName = order.Client.Name.Value,
                ContractName = order.Contract.Name.Value,
                ContractNumber = order.Contract.Number.Value,
                ContractLot = order.Contract.Lot.Value,
                ContractUnit = order.Contract.Unit.Value,
                ContractCurrency = order.Contract.Currency.Value,
                CalendarYear = order.Calendar.Date.Year.ToString(),
                CalendarMonth = order.Calendar.Date.Month.ToString(),
                CalendarDay = order.Calendar.Date.Day.ToString()
            })
            .ToListAsync(cancellationToken);

        if (orders.Count > 0)
        {
            var result = new PagedResult<SearchOrdersResponse>()
            {
                Items = orders,
                PageSize = request.Request.PageSize,
                PageNumber = request.Request.PageNumber,
                TotalCount = totalCount,
                FilteredCount = filteredCount
            };

            return result;
        }

        return Result.Failure<PagedResult<SearchOrdersResponse>>(OrderErrors.NotFound);
    }

    private IQueryable<Order> ApplyFilters(IQueryable<Order> queryable, SearchOrdersRequest request)
    {
        if (request.DateFrom.HasValue)
        {
            queryable = queryable.Where(o => o.ReceiveDate.Date >= request.DateFrom.Value.ToDateTime(TimeOnly.MinValue).ToUniversalTime());
        }

        if (request.DateTo.HasValue)
        {
            queryable = queryable.Where(o => o.ReceiveDate.Date <= request.DateTo.Value.ToDateTime(TimeOnly.MaxValue).ToUniversalTime());
        }

        if (request.Direction != null && request.Direction.Count > 0)
        {
            var groupTypes = request.Direction.Cast<OrderDirectionType>().ToList();
            queryable = queryable.Where(o => groupTypes.Contains(o.Direction));
        }

        return queryable;
    }
}