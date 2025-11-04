using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Contracts;

namespace UzEx.Analytics.Application.Deals.GetImportDealsSum;

public sealed class GetImportDealsSumQueryHandler :  IQueryHandler<GetImportDealsSumQuery, GetImportDealsSumResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public GetImportDealsSumQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<GetImportDealsSumResponse>> Handle(GetImportDealsSumQuery request, CancellationToken cancellationToken)
    {
        var amountSum = await _dbContext
            .Deals
            .Where(deal => deal.Contract != null && deal.Contract.Type == ContractType.Import)
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

        var response = new GetImportDealsSumResponse()
        {
            Sum = sum,
            Unit = unit,
        };

        return response;
    }
}