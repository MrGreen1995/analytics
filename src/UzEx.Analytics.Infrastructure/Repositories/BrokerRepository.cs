using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Domain.Brokers;

namespace UzEx.Analytics.Infrastructure.Repositories;

public class BrokerRepository : Repository<Broker>, IBrokerRepository
{
    public BrokerRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Broker?> GetByNewSpotKeyAsync(string key, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Broker>()
            .AsNoTracking()
            .FirstOrDefaultAsync(broker => broker.NewSpotKey == new BrokerNewSpotKey(key), cancellationToken);
    }

    public async Task<Broker?> GetByOldSpotKeyAsync(string key, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Broker>()
            .AsNoTracking()
            .FirstOrDefaultAsync(broker => broker.OldSpotKey == new BrokerOldSpotKey(key), cancellationToken);
    }
}