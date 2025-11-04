using System.Text.Json.Serialization;

namespace UzEx.Analytics.Infrastructure.Dtos.NewSpot;

public sealed class OrderDto
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("date")]
    public DateTime Date { get; init; }

    [JsonPropertyName("contractId")]
    public long ContractId { get; init; }

    [JsonPropertyName("clientId")]
    public required string ClientId { get; init; }
    
    [JsonPropertyName("brokerId")]
    public required string BrokerId { get; init; }
    
    [JsonPropertyName("amount")]
    public decimal Amount { get; init; }
    
    [JsonPropertyName("price")]
    public decimal Price { get; init; }

    [JsonPropertyName("direction")]
    public int Direction { get; init; }
    
    [JsonPropertyName("parentId")]
    public string? ParentId { get; init; }

    [JsonPropertyName("status")]
    public int Status { get; init; }
}