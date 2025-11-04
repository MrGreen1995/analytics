using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Extensions;
using UzEx.Analytics.Application.Models.Pagination;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Brokers;

namespace UzEx.Analytics.Application.Brokers.SearchBrokers;

public class SearchBrokersQueryHandler : IQueryHandler<SearchBrokersQuery, PagedResult<SearchBrokersResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public SearchBrokersQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<PagedResult<SearchBrokersResponse>>> Handle(SearchBrokersQuery request, CancellationToken cancellationToken)
    {
        var baseQuery = _dbContext.Brokers.AsNoTracking();
        var totalCount = await baseQuery.CountAsync(cancellationToken);

        IQueryable<Broker> filteredQuery = baseQuery;

        // Filtering
        //filteredQuery = ApplyFilters(filteredQuery, request);

        var filteredCount = await filteredQuery.CountAsync(cancellationToken);

        // Sorting
        //filteredQuery = filteredQuery.ApplySorting(request.Request.SortCriteria, "DateOnUtc");

        var brokers = await filteredQuery
            .Skip((request.Request.PageNumber - 1) * request.Request.PageSize)
            .Take(request.Request.PageSize)
            .Select(broker => new SearchBrokersResponse
            {
                Id = broker.Id,
                CreatedOnUtc = broker.CreatedOnUtc,
                BusinessKey = broker.BusinessKey.Value,
                RegNumber = broker.RegNumber.Value,
                Name = broker.Name.Value,
                Number = broker.Number.Value,
                Region = broker.Region.Value
            })
            .ToListAsync(cancellationToken);

        var result = new PagedResult<SearchBrokersResponse>
        {
            Items = new List<SearchBrokersResponse>(),
            TotalCount = 0,
            FilteredCount = 0,
            PageNumber = request.Request.PageNumber,
            PageSize = request.Request.PageSize
        };

        if (brokers.Count > 0)
        {
            result.Items = brokers;
            result.TotalCount = totalCount;
            result.FilteredCount = filteredCount;
        }

        return result;
    }

    private IQueryable<Broker> ApplyFilters(IQueryable<Broker> queryable, SearchBrokersQuery request)
    {
        if (!string.IsNullOrEmpty(request.Request.Name))
        {
            queryable = queryable.Where(b => b.Name.Value.Contains(request.Request.Name));
        }

        if (!string.IsNullOrEmpty(request.Request.Number))
        {
            queryable = queryable.Where(b => b.Number.Value.Contains(request.Request.Number));
        }

        if (!string.IsNullOrEmpty(request.Request.RegNumber))
        {
            queryable = queryable.Where(b => b.RegNumber.Value.Contains(request.Request.RegNumber));
        }

        if (!string.IsNullOrEmpty(request.Request.BusinessKey))
        {
            queryable = queryable.Where(b => b.BusinessKey.Value.Contains(request.Request.BusinessKey));
        }

        if (!string.IsNullOrEmpty(request.Request.Region))
        {
            queryable = queryable.Where(b => b.Region.Value.Contains(request.Request.Region));
        }

        if (request.Request.From.HasValue)
        {
            queryable = queryable.Where(b => b.CreatedOnUtc >= request.Request.From.Value.ToDateTime(TimeOnly.MinValue).ToUniversalTime());
        }

        if (request.Request.To.HasValue)
        {
            queryable = queryable.Where(b => b.CreatedOnUtc <= request.Request.To.Value.ToDateTime(TimeOnly.MaxValue).ToUniversalTime());
        }

        return queryable;
    }
}
