using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Application.Deals.GetTotalDealSumByMonth;

public sealed class GetTotalDealSumByMonthQueryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetTotalDealSumByMonthQuery, IReadOnlyList<GetTotalDealSumByMonthResponse>>
{
    public async Task<Result<IReadOnlyList<GetTotalDealSumByMonthResponse>>> Handle(GetTotalDealSumByMonthQuery request, CancellationToken cancellationToken)
    {
        var data = await dbContext.Deals
            .AsNoTracking()
            .Where(deal => deal.Calendar != null && deal.Calendar.Date.Year == request.Year)
            .GroupBy(deal => deal.Calendar!.Date.Month)
            .Select(group => new GetTotalDealSumByMonthResponse
            {
                Month = group.Key,
                Cost = group.Sum(d => d.Cost.Amount)
            }).ToListAsync(cancellationToken);

        foreach (var item in data)
        {
            switch (item.Cost)
            {
                case >= 1000 and < 1000000:
                    item.CostConverted = Math.Round(item.Cost / 1000, 2);
                    item.CostUnit = "тыс.";
                    break;
                case >= 1000000 and < 1000000000:
                    item.CostConverted = Math.Round(item.Cost / 1000000, 2);
                    item.CostUnit = "млн.";
                    break;
                case >= 1000000000 and < 1000000000000:
                    item.CostConverted = Math.Round(item.Cost / 1000000000, 2);
                    item.CostUnit = "млрд.";
                    break;
                case >= 1000000000000 and < 1000000000000000:
                    item.CostConverted = Math.Round(item.Cost / 1000000000000, 2);
                    item.CostUnit = "трлн.";
                    break;
                default:
                    item.CostConverted = Math.Round(item.Cost, 2);
                    item.CostUnit = string.Empty;
                    break;
            }
        }

        return data;
    }
}