using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Domain.Clients;

namespace UzEx.Analytics.Infrastructure.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Client?> GetRezidentByRegNumAsync(string regNum, CancellationToken cancellationToken = default)
        {
            return await DbContext
                .Set<Client>()
                .AsNoTracking()
                .FirstOrDefaultAsync(client => client.Type == ClientType.Rezident 
                                           && client.RegNumber == new ClientRegNumber(regNum), cancellationToken);
        }

        public async Task<Client?> GetByNewSpotKeyAsync(string key, CancellationToken cancellationToken = default)
        {
            return await DbContext
                .Set<Client>()
                .AsNoTracking()
                .FirstOrDefaultAsync(client => client.NewSpotKey == new ClientNewSpotKey(key), cancellationToken);
        }

        public async Task<Client?> GetByOldSpotKeyAsync(string key, CancellationToken cancellationToken = default)
        {
            return await DbContext
                .Set<Client>()
                .AsNoTracking()
                .FirstOrDefaultAsync(client => client.OldSpotKey == new ClientOldSpotKey(key), cancellationToken);
        }
    }
}
