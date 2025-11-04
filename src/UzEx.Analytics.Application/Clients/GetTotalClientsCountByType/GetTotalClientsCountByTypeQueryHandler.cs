using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Clients;

namespace UzEx.Analytics.Application.Clients.GetTotalClientsCountByType;

public sealed class GetTotalClientsCountByTypeQueryHandler : IQueryHandler<GetTotalClientsCountByTypeQuery, List<GetTotalClientsCountByTypeResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetTotalClientsCountByTypeQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<List<GetTotalClientsCountByTypeResponse>>> Handle(GetTotalClientsCountByTypeQuery request, CancellationToken cancellationToken)
    {
        var aggregatedData = await _dbContext.Clients
            .AsNoTracking()
            .Where(c => c.Type != ClientType.Undefined)
            .GroupBy(c => c.Type)
            .Select(g => new
            {
                ClientTypeEnum = g.Key,
                Count = g.Count()
            })
            .ToListAsync(cancellationToken);

        var result = aggregatedData
            .Select(x => new GetTotalClientsCountByTypeResponse()
            {
                ClientType = Enum.GetName(typeof(ClientType), x.ClientTypeEnum)!,
                Count = x.Count
            })
            .OrderBy(r => r.Count)
            .ToList();

        return result;
    }
}
