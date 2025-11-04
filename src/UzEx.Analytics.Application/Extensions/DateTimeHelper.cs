namespace UzEx.Analytics.Application.Extensions;

public static class DateTimeHelper
{

    /// <summary>
    /// Generates a list of all Year and Month indices between the start and end dates (inclusive).
    /// </summary>
    public static List<DateTime> GetMonthsInRange(DateTime startDate, DateTime endDate)
    {
        var months = new List<DateTime>();

        // Ensure start date is the beginning of the month
        var current = new DateTime(startDate.Year, startDate.Month, 1);

        // Ensure end date is the beginning of its month for comparison
        var end = new DateTime(endDate.Year, endDate.Month, 1);

        while (current <= end)
        {
            months.Add(current);
            current = current.AddMonths(1);
        }

        // We project to an anonymous type here to match the aggregatedData structure
        return months;
    }
}
