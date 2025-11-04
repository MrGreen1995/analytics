using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Application.Deals.GetTotalDealsCount;

public sealed class GetTotalDealsCountQueryHandler: IQueryHandler<GetTotalDealsCountQuery, GetTotalDealsCountResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public GetTotalDealsCountQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<GetTotalDealsCountResponse>> Handle(GetTotalDealsCountQuery request, CancellationToken cancellationToken)
    {
        var dealsCount = await _dbContext
            .Deals
            .AsNoTracking()
            .LongCountAsync(cancellationToken);

        var count = 0m;
        var unit = string.Empty;
        
        switch (dealsCount)
        {
            case < 1000:
                count = dealsCount;
                unit = string.Empty;
                break;
            case >= 1000 and < 1000000:
                count = Math.Round((decimal)dealsCount / 1000, 2);
                unit = "тыс.";
                break;
            case >= 1000000 and < 1000000000:
                count =  Math.Round((decimal)dealsCount / 1000000, 2);
                unit = "млн.";
                break;
            case >= 1000000000 and < 1000000000000:
                count =  Math.Round((decimal)dealsCount / 1000000000, 2);
                unit = "млрд.";
                break;
            case >= 1000000000000 and < 1000000000000000:
                count =  Math.Round((decimal)dealsCount / 1000000000000, 2);
                unit = "трлн.";
                break;
        }

        var response = new GetTotalDealsCountResponse
        {
            Count = count,
            Unit = unit
        };

        return response;
    }
}