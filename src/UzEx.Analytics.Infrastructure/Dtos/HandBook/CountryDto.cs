using System.Text.Json.Serialization;

namespace UzEx.Analytics.Infrastructure.Dtos.HandBook;

public sealed class CountryDto
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("shortName")]
    public required  string ShortName { get; init; }

    [JsonPropertyName("code")]
    public required string Code { get; init; }

    [JsonPropertyName("number")]
    public required string Number { get; init; }

    [JsonPropertyName("oftenUse")]
    public bool OftenUse { get; init; }
}
