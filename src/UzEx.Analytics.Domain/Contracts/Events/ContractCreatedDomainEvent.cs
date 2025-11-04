using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Domain.Contracts.Events
{
    public record ContractCreatedDomainEvent(Guid Id) : IDomainEvent;
}
