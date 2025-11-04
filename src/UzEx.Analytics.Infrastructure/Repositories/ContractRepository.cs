using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Domain.Contracts;
using UzEx.Analytics.Domain.Shared;

namespace UzEx.Analytics.Infrastructure.Repositories;

internal sealed class ContractRepository : Repository<Contract>, IContractRepository
{
    public ContractRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<Contract>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Contract>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Contract?> GetByBusinessKeyAsync(string businessKey, ContractPlatformType platformType,
        CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Contract>()
            .AsNoTracking()
            .FirstOrDefaultAsync(contract => contract.Platform == platformType
                                          && contract.BusinessKey == new BusinessKey(businessKey), cancellationToken);
    }
}