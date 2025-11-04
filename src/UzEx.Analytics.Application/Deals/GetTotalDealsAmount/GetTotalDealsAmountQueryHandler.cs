using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Application.Deals.GetTotalDealsAmount;

public class GetTotalDealsAmountQueryHandler : IQueryHandler<GetTotalDealsAmountQuery, GetTotalDealsAmountResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public GetTotalDealsAmountQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<GetTotalDealsAmountResponse>> Handle(GetTotalDealsAmountQuery request, CancellationToken cancellationToken)
    {
        var amounts = await _dbContext
            .Deals
            .AsNoTracking()
            .Select(deal => deal.Amount.Value)
            .ToListAsync(cancellationToken);
        
        var amount = 0m;
        var unit = string.Empty;
        
        var amountSum = amounts.Sum();
        
        switch (amountSum)
        {
            case >= 1000 and < 1000000:
                amount = Math.Round(amountSum / 1000, 2);
                unit = "тыс.";
                break;
            case >= 1000000 and < 1000000000:
                amount = Math.Round(amountSum/ 1000000, 2);
                unit = "млн.";
                break;
            case >= 1000000000 and < 1000000000000:
                amount = Math.Round(amountSum / 1000000000, 2);
                unit = "млрд.";
                break;
            case >= 1000000000000 and < 1000000000000000:
                amount = Math.Round(amountSum / 1000000000000, 2);
                unit = "трлн.";
                break;
        }

        var response = new GetTotalDealsAmountResponse
        {
            Amount = amount,
            Unit = unit,
        };

        return response;
    }
}