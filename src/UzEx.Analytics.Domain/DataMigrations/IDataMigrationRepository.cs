namespace UzEx.Analytics.Domain.DataMigrations;

public interface IDataMigrationRepository
{
    Task<DataMigration?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(DataMigration dataMigration);
}