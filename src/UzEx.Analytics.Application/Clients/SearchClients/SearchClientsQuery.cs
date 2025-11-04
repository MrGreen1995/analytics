using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Models.Pagination;

namespace UzEx.Analytics.Application.Clients.SearchClients
{
    public sealed record SearchClientsQuery(SearchClientsRequest Request) : IQuery<PagedResult<SearchClientsResponse>>;
}
