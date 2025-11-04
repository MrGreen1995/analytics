using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Extensions;
using UzEx.Analytics.Application.Models.Pagination;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Contracts;

namespace UzEx.Analytics.Application.Contracts.SearchContracts
{
    internal sealed class SearchContractsQueryHandler : IQueryHandler<SearchContractsQuery, PagedResult<SearchContractsResponse>>
    {
        private readonly IApplicationDbContext _dbContext;

        public SearchContractsQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Result<PagedResult<SearchContractsResponse>>> Handle(SearchContractsQuery request, CancellationToken cancellationToken)
        {
            var baseQuery = _dbContext.Contracts.AsNoTracking();

            var totalCount = await baseQuery.CountAsync(cancellationToken);

            var filteredQuery = baseQuery;

            // Filtering
            filteredQuery = ApplyFilters(filteredQuery, request);

            var filteredCount = await filteredQuery.CountAsync(cancellationToken);

            // Sorting
            filteredQuery = filteredQuery.ApplySorting(request.Request.SortCriteria, "CreatedOnUtc");

            var contracts = await filteredQuery
                .Skip((request.Request.PageNumber - 1) * request.Request.PageSize)
                .Take(request.Request.PageSize)
                .Select(contract => new SearchContractsResponse
                {
                    Id = contract.Id,
                    BusinessKey = contract.BusinessKey.Value,
                    Number = contract.Number.Value,
                    BasePrice = contract.BasePrice.Value,
                    Form = contract.Form.ToString(),
                    Lot = contract.Lot.Value,
                    Type = contract.Type.ToString(),
                    Unit = contract.Unit.Value,
                    Currency = contract.Currency.Value,
                    TradeType = contract.TradeType.ToString(),
                    Warehouse = contract.Warehouse.Value
                })
                .ToListAsync(cancellationToken);

            var result = new PagedResult<SearchContractsResponse>
            {
                Items = contracts,
                TotalCount = totalCount,
                FilteredCount = filteredCount,
                PageNumber = request.Request.PageNumber,
                PageSize = request.Request.PageSize
            };

            return result;

        }

        private IQueryable<Contract> ApplyFilters(IQueryable<Contract> queryable, SearchContractsQuery request)
        {
            if (!string.IsNullOrEmpty(request.Request.ContractNumber))
            {
                queryable = queryable.Where(c => c.Number.Value.Contains(request.Request.ContractNumber));
            }

            if (!string.IsNullOrEmpty(request.Request.ContractName))
            {
                queryable = queryable.Where(c => c.Name.Value.Contains(request.Request.ContractName));
            }

            if (!string.IsNullOrEmpty(request.Request.ContractUnit))
            {
                queryable = queryable.Where(c => c.Unit.Value.Contains(request.Request.ContractUnit));
            }

            if (!string.IsNullOrEmpty(request.Request.ContractCurrency))
            {
                queryable = queryable.Where(c => c.Currency.Value.Contains(request.Request.ContractCurrency));
            }

            if (!string.IsNullOrEmpty(request.Request.ContractDeliveryBase))
            {
                queryable = queryable.Where(c => c.DeliveryBase.Value.Contains(request.Request.ContractDeliveryBase));
            }

            if (request.Request.ContractType.Count > 0)
            {
                var contractTypes = request.Request.ContractType.Cast<ContractType>().ToList();
                queryable = queryable.Where(c => contractTypes.Contains(c.Type));
            }

            if (request.Request.ContractForm.Count > 0)
            {
                var contractForms = request.Request.ContractForm.Cast<ContractForm>().ToList();
                queryable = queryable.Where(c => contractForms.Contains(c.Form));
            }

            if (request.Request.ContractTradeType.Count > 0)
            {
                var contractTradeTypes = request.Request.ContractTradeType.Cast<ContractTradeType>().ToList();
                queryable = queryable.Where(c => contractTradeTypes.Contains(c.TradeType));
            }

            return queryable;
        }

    }
}