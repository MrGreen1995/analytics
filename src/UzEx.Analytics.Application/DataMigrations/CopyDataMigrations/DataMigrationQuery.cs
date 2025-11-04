using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.DataMigrations;

namespace UzEx.Analytics.Application.DataMigrations.CopyDataMigrations;

public sealed record DataMigrationQuery(
    DateOnly Date, 
    DataMigrationPlatformType PlatformType, 
    DataMigrationDataType DataType) : IQuery<bool>;