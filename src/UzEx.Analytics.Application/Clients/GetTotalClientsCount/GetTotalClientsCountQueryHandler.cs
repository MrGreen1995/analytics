using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Application.Clients.GetTotalClientsCount;

public sealed class GetTotalClientsCountQueryHandler : IQueryHandler<GetTotalClientsCountQuery, GetTotalClientsCountResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public GetTotalClientsCountQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<GetTotalClientsCountResponse>> Handle(GetTotalClientsCountQuery request, CancellationToken cancellationToken)
    {
        var clientsCount = await _dbContext.Clients
            .Where(c => c.Type != Domain.Clients.ClientType.Undefined)
            .CountAsync(cancellationToken);

        var response = new GetTotalClientsCountResponse()
        {
            Count = clientsCount
        };

        return response;
    }
}
