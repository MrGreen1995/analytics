using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Orders;

namespace UzEx.Analytics.Application.Dashboards.GetExchangeVolumes;

public sealed class GetExchangeVolumesQueryHandler : IQueryHandler<GetExchangeVolumesQuery, GetExchangeVolumesResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public GetExchangeVolumesQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<GetExchangeVolumesResponse>> Handle(GetExchangeVolumesQuery query, CancellationToken cancellationToken)
    {
        var start = new DateTime(query.StartDate.Year, query.StartDate.Month, query.StartDate.Day);
        var end = new DateTime(query.EndDate.Year, query.EndDate.Month, query.EndDate.Day);
        var ctrTypes = query.ContractTypes;
        
        var dealVolume = await _dbContext
            .Deals
            .Where(deal => deal.Contract != null
                        && deal.Contract.TradeType == query.TradeType
                        && deal.DateOnUtc >= start.ToUniversalTime()
                        && deal.DateOnUtc <= end.ToUniversalTime()
                        && ctrTypes.Contains(deal.Contract.Type))
            .AsNoTracking()
            .SumAsync(deal => deal.Cost.Amount, cancellationToken);

        var volume = 0m;
        var volumeUnit = string.Empty;
        
        switch (dealVolume)
        {
            case >= 1000 and < 1000000:
                volume = Math.Round(dealVolume / 1000, 2);
                volumeUnit = "тыс.";
                break;
            case >= 1000000 and < 1000000000:
                volume = Math.Round(dealVolume/ 1000000, 2);
                volumeUnit = "млн.";
                break;
            case >= 1000000000 and < 1000000000000:
                volume = Math.Round(dealVolume / 1000000000, 2);
                volumeUnit = "млрд.";
                break;
            case >= 1000000000000 and < 1000000000000000:
                volume = Math.Round(dealVolume / 1000000000000, 2);
                volumeUnit = "трлн.";
                break;
        }
        
        var response = new GetExchangeVolumesResponse
        {
            Trade = query.TradeType.ToString(),
            Volume = volume,
            VolumeUnit = volumeUnit
        };

        return response;
    }
}