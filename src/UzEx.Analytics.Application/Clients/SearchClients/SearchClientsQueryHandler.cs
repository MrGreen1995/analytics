using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Extensions;
using UzEx.Analytics.Application.Models.Pagination;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Clients;
using UzEx.Analytics.Domain.Clients.Errors;

namespace UzEx.Analytics.Application.Clients.SearchClients
{
    internal class SearchClientsQueryHandler : IQueryHandler<SearchClientsQuery, PagedResult<SearchClientsResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public SearchClientsQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<PagedResult<SearchClientsResponse>>> Handle(SearchClientsQuery request, CancellationToken cancellationToken)
        {
            var baseQuery = _dbContext.Clients
                .AsQueryable()
                .AsNoTracking();

            var totalCount = await baseQuery.CountAsync(cancellationToken);

            IQueryable<Client> filteredQuery = baseQuery;

            // Filtering
            //filteredQuery = ApplyFilters(filteredQuery, request.Request);

            var filteredCount = await filteredQuery.CountAsync(cancellationToken);

            // Sorting
            filteredQuery = filteredQuery.ApplySorting(request.Request.SortCriteria, "CreatedOnUtc");

            // paging & projection
            var clients = await filteredQuery
                .Skip((request.Request.PageNumber - 1) * request.Request.PageSize)
                .Take(request.Request.PageSize)
                .Select(client => new SearchClientsResponse
                {
                    Id = client.Id,
                    CreatedOnUtc = client.CreatedOnUtc,
                    RegNumber = client.RegNumber.Value,
                    Name = client.Name.Value,
                    Type = client.Type,
                    Country = client.Country.Value,
                    Region = client.Region.Value,
                    District = client.District.Value,
                    Address = client.Address.Value

                })
                .ToListAsync(cancellationToken);

            if (clients.Count > 0)
            {
                var result = new PagedResult<SearchClientsResponse>
                {
                    Items = clients,
                    TotalCount = totalCount,
                    FilteredCount = filteredCount,
                    PageNumber = request.Request.PageNumber,
                    PageSize = request.Request.PageSize
                };

                return result;
            }

            return Result.Failure<PagedResult<SearchClientsResponse>>(ClientErrors.NotFound);
        }

        private IQueryable<Client> ApplyFilters(IQueryable<Client> queryable, SearchClientsRequest request)
        {
            if (request.From.HasValue)
            {
                queryable = queryable.Where(c => c.CreatedOnUtc >= request.From.Value.ToDateTime(TimeOnly.MinValue).ToUniversalTime());
            }

            if (request.To.HasValue)
            {
                queryable = queryable.Where(c => c.CreatedOnUtc <= request.To.Value.ToDateTime(TimeOnly.MaxValue).ToUniversalTime());
            }

            if (!string.IsNullOrEmpty(request.RegNumber))
            {
                queryable = queryable.Where(c => c.RegNumber.Value == request.RegNumber.Trim());
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                queryable = queryable.Where(c => c.Name.Value == request.Name.Trim());
            }

            if (!string.IsNullOrEmpty(request.Country))
            {
                queryable = queryable.Where(c => c.Country.Value == request.Country.Trim());
            }

            if (!string.IsNullOrEmpty(request.Region))
            {
                queryable = queryable.Where(c => c.Region.Value == request.Region.Trim());
            }

            if (!string.IsNullOrEmpty(request.District))
            {
                queryable = queryable.Where(c => c.District.Value == request.District.Trim());
            }

            if (!string.IsNullOrEmpty(request.Address))
            {
                queryable = queryable.Where(c => c.Address.Value == request.Address.Trim());
            }

            if (request.Type != null && request.Type.Count() > 0)
            {
                var groupType = request.Type.Cast<ClientType>();
                queryable = queryable.Where(c => groupType.Contains(c.Type));
            }

            return queryable;
        }
    }
}
