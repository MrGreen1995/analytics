using System.Text.Json.Serialization;

namespace UzEx.Analytics.Infrastructure.Dtos.NewSpot;

public sealed class NewSpotContractDto
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("productId")]
    public long? ProductId { get; init; }

    [JsonPropertyName("number")]
    public required string Number { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("tradeType")]
    public int TradeType { get; init; }
    
    [JsonPropertyName("type")]
    public int Type { get; init; }
    
    [JsonPropertyName("form")]
    public int Form { get; init; }

    [JsonPropertyName("lot")]
    public decimal Lot { get; init; }

    [JsonPropertyName("unit")]
    public required string Unit{ get; init; }
    
    [JsonPropertyName("basePrice")]
    public decimal BasePrice { get; init; }
    
    [JsonPropertyName("currency")]
    public required string Currency { get; init; }
    
    [JsonPropertyName("deliveryBase")]
    public required string DeliveryBase { get; init; }
    
    [JsonPropertyName("warehouse")]
    public required string Warehouse { get; init; }
    
    [JsonPropertyName("originCountry")]
    public required string OriginCountry { get; init; }
}