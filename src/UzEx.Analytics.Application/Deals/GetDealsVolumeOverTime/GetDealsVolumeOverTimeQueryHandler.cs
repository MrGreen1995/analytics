using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Application.Deals.GetDealsVolumeOverTime;

public sealed class GetDealsVolumeOverTimeQueryHandler
    : IQueryHandler<GetDealsVolumeOverTimeQuery, GetDealsVolumeOverTimeResponse>
{

    private readonly IApplicationDbContext _dbContext;

    public GetDealsVolumeOverTimeQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<GetDealsVolumeOverTimeResponse>> Handle(GetDealsVolumeOverTimeQuery request, CancellationToken cancellationToken)
    {
        var startDate = request.Request.StartDate.ToDateTime(TimeOnly.MinValue);
        var endDate = request.Request.EndDate.ToDateTime(TimeOnly.MaxValue);

        var volume = await _dbContext.Deals
            .Where(d => d.DateOnUtc >= startDate.ToUniversalTime() && d.DateOnUtc <= endDate.ToUniversalTime())
            .SumAsync(d => d.Amount.Value, cancellationToken);

        var result = new GetDealsVolumeOverTimeResponse
        {
            Volume = volume
        };

        return result;
    }
}
