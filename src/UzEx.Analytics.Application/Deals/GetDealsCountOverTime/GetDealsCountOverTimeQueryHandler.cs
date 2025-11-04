using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Application.Deals.GetDealsCountOverTime;

public sealed class GetDealsCountOverTimeQueryHandler : IQueryHandler<GetDealsCountOverTimeQuery, GetDealsCountOverTimeResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public GetDealsCountOverTimeQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }



    public async Task<Result<GetDealsCountOverTimeResponse>> Handle(GetDealsCountOverTimeQuery request, CancellationToken cancellationToken)
    {
        var startDate = request.Request.StartDate.ToDateTime(TimeOnly.MinValue);
        var endDate = request.Request.EndDate.ToDateTime(TimeOnly.MaxValue);

        var count = await _dbContext.Deals
            .Where(d => d.DateOnUtc >= startDate.ToUniversalTime() && d.DateOnUtc <= endDate.ToUniversalTime())
            .LongCountAsync(cancellationToken);

        var result = new GetDealsCountOverTimeResponse
        {
            Count = count
        };

        return result;
    }
}
