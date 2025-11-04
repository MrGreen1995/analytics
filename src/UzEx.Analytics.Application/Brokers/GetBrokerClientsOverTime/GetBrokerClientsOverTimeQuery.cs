using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Models.Pagination;

namespace UzEx.Analytics.Application.Brokers.GetBrokerClientsOverTime;

public sealed record GetBrokerClientsOverTimeQuery(GetBrokerClientsOverTimeRequest Request)
    : IQuery<PagedResult<GetBrokerClientsOverTimeResponse>>;
