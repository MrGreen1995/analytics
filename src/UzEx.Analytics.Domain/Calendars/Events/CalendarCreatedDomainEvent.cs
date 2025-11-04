using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Domain.Calendars.Events;

public record CalendarCreatedDomainEvent(Guid Id) : IDomainEvent;