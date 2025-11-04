using System.Text.Json.Serialization;

namespace UzEx.Analytics.Infrastructure.Dtos.HandBook;

public sealed class RegionDto
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("code")]
    public required string Code { get; init; }

    [JsonPropertyName("number")]
    public int? Number { get; init; }

    [JsonPropertyName("shortName")]
    public string? ShortName { get; init; }

    [JsonPropertyName("countryId")]
    public int CountryId { get; init; }

    [JsonPropertyName("countryName")]
    public string? CountryName { get; init; }

    [JsonPropertyName("countryShortName")]
    public string? CountryShortName { get; init; }
}
