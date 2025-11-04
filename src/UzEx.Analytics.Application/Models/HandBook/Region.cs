namespace UzEx.Analytics.Application.Models.HandBook;

public sealed class Region
{
    public int Id { get; init; }

    public required string Name { get; init; }

    public required string Code { get; init; }

    public int? Number { get; init; }

    public string? ShortName { get; init; }

    public int CountryId { get; init; }

    public string? CountryName { get; init; }

    public string? CountryShortName { get; set; }
}
