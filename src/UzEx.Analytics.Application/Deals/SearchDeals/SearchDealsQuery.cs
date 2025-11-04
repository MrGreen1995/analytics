using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Models.Pagination;

namespace UzEx.Analytics.Application.Deals.SearchDeals
{
    public sealed record SearchDealsQuery(SearchDealsRequest Request) : IQuery<PagedResult<SearchDealsResponse>>;
}
