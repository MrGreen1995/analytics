using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Brokers.BrokerClients.Shared;
using UzEx.Analytics.Application.Models.Pagination;

namespace UzEx.Analytics.Application.Brokers.BrokerClients.GetBuyerBrokerClientsOverTime;

public sealed record GetBuyerBrokerClientOverTimeQuery(GetBrokerClientsOverTimeRequest Request)
    : IQuery<PagedResult<GetBrokerClientsOverTimeResponse>>;
