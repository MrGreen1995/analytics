namespace UzEx.Analytics.Api.Controllers.DataMigrations;

public sealed record DataMigrationRequest(DateOnly Date, int Platform, int DataType);