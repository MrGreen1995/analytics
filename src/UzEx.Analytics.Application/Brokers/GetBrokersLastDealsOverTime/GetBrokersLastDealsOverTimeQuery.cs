using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Models.Pagination;

namespace UzEx.Analytics.Application.Brokers.GetBrokersLastDealsOverTime;

public sealed record GetBrokersLastDealsOverTimeQuery(GetBrokersLastDealsOverTimeRequest Request)
    : IQuery<PagedResult<GetBrokersLastDealsOverTimeResponse>>;

