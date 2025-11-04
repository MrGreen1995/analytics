using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.DataMigrations.Events;

namespace UzEx.Analytics.Domain.DataMigrations;

public sealed class DataMigration : Entity
{
    public DateTime CreatedOnUtc { get; private set; }

    public DataMigrationPlatformType PlatformType { get; private set; }
    
    public DataMigrationDataType DataType { get; private set; }
    
    public DataMigrationPayload Payload { get; private set; }
    
    private DataMigration(){}

    public DataMigration(
        Guid id,
        DateTime createdOnUtc, 
        DataMigrationPlatformType platformType,
        DataMigrationDataType dataType, 
        DataMigrationPayload payload) : base(id)
    {
        CreatedOnUtc = createdOnUtc;
        PlatformType = platformType;
        DataType = dataType;
        Payload = payload;
    }

    public static DataMigration Create(
        Guid id,
        DateTime createdOnUtc, 
        DataMigrationPlatformType platformType, 
        DataMigrationDataType dataType,
        string payload)
    {
        var dataMigration = new DataMigration(
            id,
            createdOnUtc,
            platformType,
            dataType,
            new  DataMigrationPayload(payload)
        );
        
        dataMigration.RaiseDomainEvent(new DataMigrationCreatedDomainEvent(dataMigration.Id));
        
        return dataMigration;
    }
}