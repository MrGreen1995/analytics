using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Models.Pagination;

namespace UzEx.Analytics.Application.Orders.SearchOrders;

public sealed record SearchOrdersQuery(SearchOrdersRequest Request) : IQuery<PagedResult<SearchOrdersResponse>>;