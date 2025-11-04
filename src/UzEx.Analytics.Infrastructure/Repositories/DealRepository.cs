using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Domain.Deals;
using UzEx.Analytics.Domain.Shared;

namespace UzEx.Analytics.Infrastructure.Repositories
{
    public class DealRepository : Repository<Deal>, IDealRepository
    {
        public DealRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Deal?> GetByBusinessKey(string businessKey, CancellationToken cancellationToken = default)
        {
            return DbContext
                .Set<Deal>()
                .AsNoTracking()
                .FirstOrDefaultAsync(deal => deal.BusinessKey == new BusinessKey(businessKey), cancellationToken);
        }

        public Task<Deal?> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            return DbContext
                .Set<Deal>()
                .AsNoTracking()
                .FirstOrDefaultAsync(deal => deal.Id == id, cancellationToken);
        }
    }
}
