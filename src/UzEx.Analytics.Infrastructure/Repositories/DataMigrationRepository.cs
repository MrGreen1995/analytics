using UzEx.Analytics.Domain.DataMigrations;

namespace UzEx.Analytics.Infrastructure.Repositories;

public class DataMigrationRepository : Repository<DataMigration>, IDataMigrationRepository
{
    public DataMigrationRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    
}