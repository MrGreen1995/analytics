namespace UzEx.Analytics.Application.Models.HandBook;

public class District
{
    public int Id { get; init; }

    public required string Name { get; init; }

    public int Code { get; init; }

    public int? Number { get; init; }

    public string? ShortName { get; init; }

    public int RegionId { get; init; }

    public string? RegionName { get; init; }

    public string? RegionShortName { get; init; }
}
