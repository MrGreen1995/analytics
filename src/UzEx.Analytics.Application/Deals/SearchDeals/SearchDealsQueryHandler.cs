using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Extensions;
using UzEx.Analytics.Application.Models.Pagination;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Deals;
using UzEx.Analytics.Domain.Deals.Errors;

namespace UzEx.Analytics.Application.Deals.SearchDeals
{
    internal class SearchDealsQueryHandler : IQueryHandler<SearchDealsQuery, PagedResult<SearchDealsResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public SearchDealsQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<PagedResult<SearchDealsResponse>>> Handle(SearchDealsQuery request, CancellationToken cancellationToken)
        {
            var baseQuery = _dbContext.Deals
           .AsQueryable()
           .AsNoTracking();

            var totalCount = await baseQuery.CountAsync(cancellationToken);

            IQueryable<Deal> filteredQuery = baseQuery;

            // Filter
            filteredQuery = ApplyFilters(filteredQuery, request.Request);

            var filteredCount = await filteredQuery.CountAsync(cancellationToken);

            // Sorting
            filteredQuery = filteredQuery.ApplySorting(request.Request.SortCriteria, "DateOnUtc");

            // paging & projection
            var deals = await filteredQuery
                .Skip((request.Request.PageNumber - 1) * request.Request.PageSize)
                .Take(request.Request.PageSize)
                .Select(deal => new SearchDealsResponse
                {
                    Id = deal.Id,
                    CreatedOnUtc = deal.CreatedOnUtc,
                    ModifiedOnUtc = deal.ModifiedOnUtc,
                    BusinessKey = deal.BusinessKey.Value,
                    DateOnUtc = deal.DateOnUtc,
                    Number = deal.Number.Value,
                    SessionType = deal.SessionType,
                    Amount = deal.Amount.Value,
                    Price = deal.Price.Value,
                    Cost = deal.Cost.Amount,
                    Status = deal.Status,
                    PaymentDays = deal.PaymentDays.Value,
                    DeliveryDays = deal.DeliveryDays.Value,
                    PaymentDate = deal.PaymentDate.Value,
                    CloseDate = deal.CloseDate.Value,
                    AnnulDate = deal.AnnulDate.Value,
                    AnnulReason = deal.AnnulReason
                })
                .ToListAsync(cancellationToken);

            if (deals.Count > 0)
            {
                var result = new PagedResult<SearchDealsResponse>()
                {
                    Items = deals,
                    PageNumber = request.Request.PageNumber,
                    PageSize = request.Request.PageSize,
                    FilteredCount = filteredCount,
                    TotalCount = totalCount
                };

                return result;
            }

            return Result.Failure<PagedResult<SearchDealsResponse>>(DealErrors.NotFound);
        }

        private IQueryable<Deal> ApplyFilters(IQueryable<Deal> queryable, SearchDealsRequest request)
        {
            if (request.From.HasValue)
            {
                queryable = queryable.Where(o => o.DateOnUtc.Date >= request.From.Value.ToDateTime(TimeOnly.MinValue).ToUniversalTime());
            }

            if (request.To.HasValue)
            {
                queryable = queryable.Where(o => o.DateOnUtc.Date <= request.To.Value.ToDateTime(TimeOnly.MaxValue).ToUniversalTime());
            }

            if (request.SessionType != null && request.SessionType.Count > 0)
            {
                var groupTypes = request.SessionType.Cast<DealSessionType>().ToList();
                queryable = queryable.Where(o => groupTypes.Contains(o.SessionType));
            }

            if (request.Status != null && request.Status.Count > 0)
            {
                var groupTypes = request.Status.Cast<DealStatusType>().ToList();
                queryable = queryable.Where(o => groupTypes.Contains(o.Status));
            }

            return queryable;
        }
    }
}
