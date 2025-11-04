using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Domain.DataMigrations.Events;

public record DataMigrationCreatedDomainEvent(Guid Id) : IDomainEvent;