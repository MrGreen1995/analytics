using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Brokers.Errors;

namespace UzEx.Analytics.Application.Brokers.GetBroker;

public class GetBrokerQueryHandler: IQueryHandler<GetBrokerQuery, GetBrokerResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public GetBrokerQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<GetBrokerResponse>> Handle(GetBrokerQuery request, CancellationToken cancellationToken)
    {
        var broker = await _dbContext.Brokers
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

        if (broker == null)
        {
            return Result.Failure<GetBrokerResponse>(BrokerErrors.NotFound);
        }

        var response = new GetBrokerResponse
        {
            Id = broker.Id,
            Name = broker.Name.Value,
        };
        
        return response;
    }
}