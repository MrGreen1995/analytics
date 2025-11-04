using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Models.Pagination;

namespace UzEx.Analytics.Application.Brokers.GetBrokersLastOrdersOverTime;

public sealed record GetBrokersLastOrdersOverTimeQuery(GetBrokersLastOrdersOverTimeRequest Request)
    : IQuery<PagedResult<GetBrokersLastOrdersOverTimeResponse>>;
