using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Contracts;

namespace UzEx.Analytics.Application.Deals.GetInternalDealsSum;

public sealed class GetInternalDealsSumQueryHandler : IQueryHandler<GetInternalDealsSumQuery, GetInternalDealsSumResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public GetInternalDealsSumQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Result<GetInternalDealsSumResponse>> Handle(GetInternalDealsSumQuery request, CancellationToken cancellationToken)
    {
        var amountSum = await _dbContext
            .Deals
            .Where(deal => deal.Contract != null && deal.Contract.Type == ContractType.Internal)
            .AsNoTracking()
            .SumAsync(deal => deal.Cost.Amount, cancellationToken);
        
        var sum = amountSum;
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

        var response = new GetInternalDealsSumResponse()
        {
            Sum = sum,
            Unit = unit,
        };

        return response;
    }
}