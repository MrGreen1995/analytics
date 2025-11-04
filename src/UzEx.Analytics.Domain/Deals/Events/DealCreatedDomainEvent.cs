using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Domain.Deals.Events
{
    public record DealCreatedDomainEvent(Guid Id) : IDomainEvent;    
}
