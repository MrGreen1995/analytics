using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Domain.DataMigrations.Errors;

public class DataMigrationErrors
{
    public static Error NotFound = new ("DatMigration.Found", "DatMigration not found");
}