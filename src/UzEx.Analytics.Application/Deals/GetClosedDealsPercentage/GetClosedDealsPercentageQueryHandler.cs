using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Deals;

namespace UzEx.Analytics.Application.Deals.GetClosedDealsPercentage;

public sealed class GetClosedDealsPercentageQueryHandler :  IQueryHandler<GetClosedDealsPercentageQuery, GetClosedDealsPercentageResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public GetClosedDealsPercentageQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Result<GetClosedDealsPercentageResponse>> Handle(GetClosedDealsPercentageQuery request, CancellationToken cancellationToken)
    {
        var totalCount = await _dbContext
            .Deals
            .AsNoTracking()
            .LongCountAsync(cancellationToken);
        
        var totalClosedCount = await _dbContext
            .Deals
            .Where(deal => deal.Status == DealStatusType.Completed
                           || deal.Status == DealStatusType.PartialCompleted)
            .AsNoTracking()
            .LongCountAsync(cancellationToken);
        
        var percent = Math.Round(((decimal)totalClosedCount) / totalCount * 100m, 2);
       
        var response = new GetClosedDealsPercentageResponse()
        {
            Amount = totalClosedCount,
            Percent = percent
        };

        return response;
    }
}