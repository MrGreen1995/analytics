using Microsoft.EntityFrameworkCore;
using System.Globalization;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Contracts;
using UzEx.Analytics.Domain.Deals;

namespace UzEx.Analytics.Application.Deals.GetRevenueByContractTypeOverTime
{
    public sealed class GetRevenueByContractTypeOverTimeQueryHandler
        : IQueryHandler<GetRevenueByContractTypeOverTimeQuery, List<GetRevenueByContractTypeOverTimeResponse>>
    {

        private readonly IApplicationDbContext _dbContext;

        public GetRevenueByContractTypeOverTimeQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<List<GetRevenueByContractTypeOverTimeResponse>>> Handle(GetRevenueByContractTypeOverTimeQuery request, CancellationToken cancellationToken)
        {
            var startDate = request.Request.StartDate.ToDateTime(TimeOnly.MinValue);
            var endDate = request.Request.EndDate.ToDateTime(TimeOnly.MaxValue);

            var acceptedStatuses = new List<DealStatusType>
          {
            DealStatusType.WaitingDelivery,
            DealStatusType.NotDelivered,
            DealStatusType.Completed,
            DealStatusType.PartialCompleted,
            DealStatusType.Concluded
         };

            var aggregatedData = await _dbContext.Deals
                .Where(d => d.DateOnUtc >= startDate.ToUniversalTime() && d.DateOnUtc <= endDate.ToUniversalTime()
                && acceptedStatuses.Contains(d.Status))
                .GroupBy(d => new
                {
                    ContractType = d.Contract.Type,
                    Year = d.DateOnUtc.Year,
                    Month = d.DateOnUtc.Month,
                })
                .Select(g => new
                {
                    g.Key.ContractType,
                    g.Key.Year,
                    g.Key.Month,
                    DealsCount = g.Count(),
                    DealsSum = g.Sum(x => x.Cost.Amount)
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var result = aggregatedData
                .GroupBy(r => new
                {
                    r.ContractType,
                    r.Year,
                })
                .Select(g => new GetRevenueByContractTypeOverTimeResponse
                {
                    ContractType = Enum.GetName(typeof(ContractType), g.Key.ContractType)!,
                    Year = g.Key.Year,
                    Data = g.OrderBy(x => x.Month)
                    .Select(x => new RevenueOfContractTypeByMonthDataItem
                    {
                        MonthIndex = x.Month,
                        MonthName = new DateTime(x.Year, x.Month, 1).ToString("MMMM", CultureInfo.InvariantCulture),
                        DealsCount = x.DealsCount,
                        DealsSum = x.DealsSum
                    })
                    .ToList()
                })
                .OrderBy(r => r.ContractType)
                .ThenBy(r => r.Year)
                .ToList();

            return result;
        }
    }
}
