namespace UzEx.Analytics.Application.Models.NewSpot;

public sealed class NewSpotOrderModel
{
    public required string Id { get; init; }

    public DateTime Date { get; init; }

    public long ContractId { get; init; }

    public required string ClientId { get; init; }
    
    public required string BrokerId { get; init; }
    
    public decimal Amount { get; init; }
    
    public decimal Price { get; init; }

    public int Direction { get; init; }
    
    public string? ParentId { get; init; }

    public int Status { get; init; }
}