using UzEx.Analytics.Application.Abstractions.Messaging;

namespace UzEx.Analytics.Application.DataMigrations.ProcessDataMigrations;

public sealed record DataMigrationProcessQuery(Guid Id) : IQuery<bool>;