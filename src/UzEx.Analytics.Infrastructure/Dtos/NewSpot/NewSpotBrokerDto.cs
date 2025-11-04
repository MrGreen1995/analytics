using System.Text.Json.Serialization;

namespace UzEx.Analytics.Infrastructure.Dtos.NewSpot;

public sealed class NewSpotBrokerDto
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }
    
    [JsonPropertyName("accountType")]
    public int AccountType { get; init; }
    
    [JsonPropertyName("tin")]
    public required string Tin { get; init; }
    
    [JsonPropertyName("brokerNumber")]
    public required string BrokerNumber { get; init; }
    
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    
    [JsonPropertyName("countryCode")]
    public string? CountryCode { get; init; }
    
    [JsonPropertyName("regionCode")]
    public string? RegionCode { get; init; }
    
    [JsonPropertyName("districtCode")]
    public string? DistrictCode { get; init; }
    
    [JsonPropertyName("address")]
    public string? Address { get; init; }
}