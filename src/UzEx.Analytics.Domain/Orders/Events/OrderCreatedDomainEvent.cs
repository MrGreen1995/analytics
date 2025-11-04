using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Domain.Orders.Events;

public record OrderCreatedDomainEvent(Guid Id) : IDomainEvent;