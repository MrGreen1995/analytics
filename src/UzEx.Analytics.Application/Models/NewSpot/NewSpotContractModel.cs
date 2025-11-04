namespace UzEx.Analytics.Application.Models.NewSpot;

public sealed class NewSpotContractModel
{
    public long Id { get; init; }
    
    public long? ProductId { get; init; }
    
    public required string Number { get; init; }
    
    public required string Name { get; init; }
    
    public int TradeType { get; init; }
    
    public int Type { get; init; }
    
    public int Form { get; init; }
    
    public decimal Lot { get; init; }

    public required string Unit { get; set; }

    public decimal BasePrice { get; set; }

    public required string Currency { get; set; }
    
    public required string DeliveryBase { get; set; }
    
    public required string Warehouse { get; set; }
    
    public required string OriginCountryId { get; set; }
}