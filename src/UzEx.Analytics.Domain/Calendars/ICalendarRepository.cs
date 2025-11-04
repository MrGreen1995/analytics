namespace UzEx.Analytics.Domain.Calendars;

public interface ICalendarRepository
{
    Task<Calendar?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Calendar?> GetByDateKeyAsync(int dateKey, CancellationToken cancellationToken = default);
    void Add(Calendar  calendar);
}