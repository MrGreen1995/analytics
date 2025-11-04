using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Deals;

namespace UzEx.Analytics.Application.Deals.GetAnnulatedDealsPercentage;

public sealed class GetAnnulatedDealsPercentageQueryHandler : IQueryHandler<GetAnnulatedDealsPercentageQuery, GetAnnulatedDealsPercentageResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public GetAnnulatedDealsPercentageQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Result<GetAnnulatedDealsPercentageResponse>> Handle(GetAnnulatedDealsPercentageQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _dbContext
            .Deals
            .AsNoTracking()
            .LongCountAsync(cancellationToken);
        
        var totalClosedCount = await _dbContext
            .Deals
            .Where(deal => deal.Status == DealStatusType.NotRegistered
                           || deal.Status == DealStatusType.NotPaid
                           || deal.Status == DealStatusType.NotDelivered)
            .AsNoTracking()
            .LongCountAsync(cancellationToken);
        
        var percent = Math.Round(((decimal)totalClosedCount) / totalCount * 100m, 2);
       
        var response = new GetAnnulatedDealsPercentageResponse()
        {
            Amount = totalClosedCount,
            Percent = percent
        };

        return response;
    }
}