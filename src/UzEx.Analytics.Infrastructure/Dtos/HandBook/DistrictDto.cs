using System.Text.Json.Serialization;

namespace UzEx.Analytics.Infrastructure.Dtos.HandBook;

public sealed class DistrictDto
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("code")]
    public int Code { get; init; }

    [JsonPropertyName("number")]
    public int? Number { get; init; }

    [JsonPropertyName("shortName")]
    public string? ShortName { get; init; }

    [JsonPropertyName("regionId")]
    public int RegionId { get; init; }

    [JsonPropertyName("regionName")]
    public string? RegionName { get; init; }

    [JsonPropertyName("regionShortName")]
    public string? RegionShortName { get; init; }
}
