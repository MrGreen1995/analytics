using UzEx.Analytics.Application.Abstractions.Caching;

namespace UzEx.Analytics.Application.Clients.GetTotalClientsCount;

public sealed record GetTotalClientsCountQuery : ICachedQuery<GetTotalClientsCountResponse>
{
    public string CacheKey => "clients-total-count";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}
