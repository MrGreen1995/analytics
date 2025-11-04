using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Models.Pagination;

namespace UzEx.Analytics.Application.Brokers.GetMostPassiveBrokersByRegionOverTime;

public sealed record GetMostPassiveBrokersByRegionOverTimeQuery(GetMostPassiveBrokersByRegionOverTimeRequest Request)
    : IQuery<PagedResult<GetMostPassiveBrokersByRegionOverTimeResponse>>;
