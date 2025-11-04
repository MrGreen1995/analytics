using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Domain.Brokers.Events;

public record BrokerCreatedDomainEvent(Guid Id) : IDomainEvent;