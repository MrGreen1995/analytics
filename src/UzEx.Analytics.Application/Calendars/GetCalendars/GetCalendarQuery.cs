using UzEx.Analytics.Application.Abstractions.Messaging;

namespace UzEx.Analytics.Application.Calendars.GetCalendars;

public record GetCalendarQuery(Guid Id) : IQuery<GetCalendarResponse>;