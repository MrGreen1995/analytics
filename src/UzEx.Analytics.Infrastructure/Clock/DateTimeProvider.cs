using UzEx.Analytics.Application.Abstractions.Clock;

namespace UzEx.Analytics.Infrastructure.Clock;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}