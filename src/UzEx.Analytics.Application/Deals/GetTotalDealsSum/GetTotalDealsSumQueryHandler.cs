using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Application.Deals.GetTotalDealsSum;

public class GetTotalDealsSumQueryHandler : IQueryHandler<GetTotalDealsSumQuery, GetTotalDealsSumResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public GetTotalDealsSumQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<GetTotalDealsSumResponse>> Handle(GetTotalDealsSumQuery request, CancellationToken cancellationToken)
    {
        var amountSum = await _dbContext
            .Deals
            .AsNoTracking()
            .SumAsync(a => a.Cost.Amount, cancellationToken);

        var sum = 0m;
        var unit = string.Empty;
        
        switch (amountSum)
        {
            case >= 1000 and < 1000000:
                sum = Math.Round(amountSum / 1000, 2);
                unit = "тыс.";
                break;
            case >= 1000000 and < 1000000000:
                sum = Math.Round(amountSum/ 1000000, 2);
                unit = "млн.";
                break;
            case >= 1000000000 and < 1000000000000:
                sum = Math.Round(amountSum / 1000000000, 2);
                unit = "млрд.";
                break;
            case >= 1000000000000 and < 1000000000000000:
                sum = Math.Round(amountSum / 1000000000000, 2);
                unit = "трлн.";
                break;
        }

        var response = new GetTotalDealsSumResponse
        {
            Sum = sum,
            Unit = unit
        };
        
        return response;
    }
}