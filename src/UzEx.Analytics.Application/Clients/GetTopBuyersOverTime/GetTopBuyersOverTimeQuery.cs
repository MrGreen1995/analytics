using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Models.Pagination;

namespace UzEx.Analytics.Application.Clients.GetTopBuyersOverTime;

public sealed record GetTopBuyersOverTimeQuery(GetTopBuyersOverTimeRequest Request)
    : IQuery<PagedResult<GetTopBuyersOverTimeResponse>>;