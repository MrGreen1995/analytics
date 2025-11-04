using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Models.Pagination;

namespace UzEx.Analytics.Application.Contracts.SearchContracts;

public sealed record SearchContractsQuery(SearchContractsRequest Request) : IQuery<PagedResult<SearchContractsResponse>>;