using System.Text.Json.Serialization;

namespace UzEx.Analytics.Infrastructure.Dtos.Soliq;

public sealed class SoliqUserDto
{
    [JsonPropertyName("ns10Code")]
    public int Ns10Code { get; init; }
    
    [JsonPropertyName("ns11Code")]
    public int Ns11Code { get; init; }

    [JsonPropertyName("shortName")]
    public string? ShortName { get; init; }
    
    [JsonPropertyName("tin")]
    public string? Tin { get; init; }
    
    [JsonPropertyName("name")]
    public string? Name { get; init; }
    
    [JsonPropertyName("statusCode")]
    public int StatusCode { get; init; }
    
    [JsonPropertyName("statusName")]
    public string? StatusName { get; init; }
    
    [JsonPropertyName("mfo")]
    public string? Mfo { get; init; }
    
    [JsonPropertyName("account")]
    public string? Account { get; init; }
    
    [JsonPropertyName("address")]
    public string? Address { get; init; }
    
    [JsonPropertyName("oked")]
    public string? Oked { get; init; }
    
    [JsonPropertyName("directorTin")]
    public string? DirectorTin { get; init; }
    
    [JsonPropertyName("director")]
    public string? Director { get; init; }
    
    [JsonPropertyName("accountant")]
    public string? Accountant { get; init; }
    
    [JsonPropertyName("isBudget")]
    public int IsBudget { get; init; }
}