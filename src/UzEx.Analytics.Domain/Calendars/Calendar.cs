using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Calendars.Events;
using UzEx.Analytics.Domain.Deals;
using UzEx.Analytics.Domain.Orders;

namespace UzEx.Analytics.Domain.Calendars;

public sealed class Calendar : Entity
{
    public CalendarDate Date { get; private set; }

    public DateTime CreatedOnUtc { get; set; }

    public int DateKey { get; private set; }

    public ICollection<Order>? Orders { get; set; }

    public ICollection<Deal>? Deals { get; set; }

    private Calendar()
    {
    }

    public Calendar(Guid id, CalendarDate date, DateTime createdOnUtc, int dateKey) : base(id)
    {
        Date = date;
        CreatedOnUtc = createdOnUtc;
        DateKey = dateKey;
    }

    public static Calendar Create(Guid id, DateOnly date, DateTime createdOnUtc, int dateKey)
    {
        var calendar = new Calendar(id, new CalendarDate(date.Year, date.Month, date.Day), createdOnUtc, dateKey);

        calendar.RaiseDomainEvent(new CalendarCreatedDomainEvent(calendar.Id));
        
        return calendar;
    }
}