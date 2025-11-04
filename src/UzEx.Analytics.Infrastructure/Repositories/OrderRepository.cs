using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Domain.Orders;
using UzEx.Analytics.Domain.Shared;

namespace UzEx.Analytics.Infrastructure.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Order?> GetByBusinessKeyAsync(string key, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Order>()
            .AsNoTracking()
            .FirstOrDefaultAsync(order => order.BusinessKey == new BusinessKey(key), cancellationToken);
    }
}