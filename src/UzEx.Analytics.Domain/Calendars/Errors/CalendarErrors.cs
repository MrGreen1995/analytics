using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Domain.Calendars.Errors;

public class CalendarErrors
{
    public static Error NotFound = new ("Calendar.Found", "Calendar not found");
}