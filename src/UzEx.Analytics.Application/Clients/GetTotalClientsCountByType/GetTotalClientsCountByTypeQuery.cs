using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Clients.GetTotalClientsCountByType;

public sealed record GetTotalClientsCountByTypeQuery : ICachedQuery<List<GetTotalClientsCountByTypeResponse>>
{
    public string CacheKey => "clients-total-count-by-type";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
