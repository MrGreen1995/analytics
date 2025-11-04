using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Models.Pagination;

namespace UzEx.Analytics.Application.Clients.GetTopSellersOverTime;

public sealed record GetTopSellersOverTimeQuery(GetTopSellersOverTimeRequest Request)
    : IQuery<PagedResult<GetTopSellersOverTimeResponse>>;