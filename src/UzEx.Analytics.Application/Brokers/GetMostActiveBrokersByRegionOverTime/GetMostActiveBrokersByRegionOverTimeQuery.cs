using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Models.Pagination;

namespace UzEx.Analytics.Application.Brokers.GetMostActiveBrokersByRegionOverTime;

public sealed record GetMostActiveBrokersByRegionOverTimeQuery(GetMostActiveBrokersByRegionOverTimeRequest Request)
    : IQuery<PagedResult<GetMostActiveBrokersByRegionOverTimeResponse>>;
