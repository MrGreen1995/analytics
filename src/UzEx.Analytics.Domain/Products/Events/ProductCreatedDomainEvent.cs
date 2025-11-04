using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Domain.Products.Events;

public record ProductCreatedDomainEvent(Guid Id) : IDomainEvent;
