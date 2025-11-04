using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Domain.Clients.Events
{
    public record ClientCreatedDomainEvent(Guid Id) : IDomainEvent;
}
