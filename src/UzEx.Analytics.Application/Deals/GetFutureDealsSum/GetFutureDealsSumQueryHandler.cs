using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Contracts;

namespace UzEx.Analytics.Application.Deals.GetFutureDealsSum;

public class GetFutureDealsSumQueryHandler : IQueryHandler<GetFutureDealsSumQuery, GetFutureDealsSumResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public GetFutureDealsSumQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<GetFutureDealsSumResponse>> Handle(GetFutureDealsSumQuery request, CancellationToken cancellationToken)
    {
        var amountSum = await _dbContext
            .Deals
            .Where(deal => deal.Contract != null && deal.Contract.Form == ContractForm.Futures)
            .AsNoTracking()
            .SumAsync(deal => deal.Cost.Amount, cancellationToken);
        
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

        var response = new GetFutureDealsSumResponse()
        {
            Sum = sum,
            Unit = unit,
        };

        return response;
    }
}