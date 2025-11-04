using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;

namespace UzEx.Analytics.Application.Dashboards.GetExchangeParticipants;

public sealed class GetExchangeParticipantsQueryHandler : IQueryHandler<GetExchangeParticipantsQuery, GetExchangeParticipantsResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public GetExchangeParticipantsQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<GetExchangeParticipantsResponse>> Handle(GetExchangeParticipantsQuery query, CancellationToken cancellationToken)
    {
        var start = new DateTime(query.StartDate.Year, query.StartDate.Month, query.StartDate.Day);
        var end = new DateTime(query.EndDate.Year, query.EndDate.Month, query.EndDate.Day);
        var ctrTypes = query.ContractTypes;
        
        var counts = await _dbContext
            .Orders
            .AsNoTracking()
            .Where(order => order.ParentId == null
                            && order.Contract.TradeType == query.TradeType
                            && ctrTypes.Contains(order.Contract.Type)
                            && order.ReceiveDate.Date >= start.ToUniversalTime()
                            && order.ReceiveDate.Date <= end.ToUniversalTime())
            .Select(order => order.ClientId)
            .Distinct()
            .LongCountAsync(cancellationToken);
        
        var participants = (decimal)counts;
        var participantsUnit = string.Empty;

        switch (participants)
        {
            case >= 1000 and < 1000000:
                participants = Math.Round(participants / 1000, 2);
                participantsUnit = "тыс.";
                break;
            case >= 1000000 and < 1000000000:
                participants = Math.Round(participants / 1000000, 2);
                participantsUnit = "млн.";
                break;
            case >= 1000000000 and < 1000000000000:
                participants = Math.Round(participants / 1000000000, 2);
                participantsUnit = "млрд.";
                break;
            case >= 1000000000000 and < 1000000000000000:
                participants = Math.Round(participants / 1000000000000, 2);
                participantsUnit = "трлн.";
                break;
        }

        var response = new GetExchangeParticipantsResponse()
        {
            Trade = query.TradeType.ToString(),
            Participants = participants,
            ParticipantsUnit = participantsUnit,
        };
        
        return response;
    }
}