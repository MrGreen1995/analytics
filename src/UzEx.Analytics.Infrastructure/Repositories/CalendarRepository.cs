using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Domain.Calendars;

namespace UzEx.Analytics.Infrastructure.Repositories;

public class CalendarRepository : Repository<Calendar>, ICalendarRepository
{
    public CalendarRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Calendar?> GetByDateKeyAsync(int dateKey, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<Calendar>()
            .AsNoTracking()
            .FirstOrDefaultAsync(calendar => calendar.DateKey == dateKey, cancellationToken);
    }
}