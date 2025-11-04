using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Deals;

namespace UzEx.Analytics.Application.Deals.GetPaidDealsPercentage;

public sealed class GetPaidDealsPercentageQueryHandler : IQueryHandler<GetPaidDealsPercentageQuery, GetPaidDealsPercentageResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public GetPaidDealsPercentageQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Result<GetPaidDealsPercentageResponse>> Handle(GetPaidDealsPercentageQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _dbContext
            .Deals
            .AsNoTracking()
            .LongCountAsync(cancellationToken);
        
        var totalPaidCount = await _dbContext
            .Deals
            .Where(deal => deal.Status == DealStatusType.WaitingDelivery
                        || deal.Status == DealStatusType.DeliveryExpired
                        || deal.Status == DealStatusType.Completed
                        || deal.Status == DealStatusType.PartialCompleted)
            .AsNoTracking()
            .LongCountAsync(cancellationToken);
        
        var percent = Math.Round(((decimal)totalPaidCount) / totalCount * 100m, 2);
       
        var response = new GetPaidDealsPercentageResponse()
        {
            Amount = totalPaidCount,
            Percent = percent
        };

        return response;
    }
}