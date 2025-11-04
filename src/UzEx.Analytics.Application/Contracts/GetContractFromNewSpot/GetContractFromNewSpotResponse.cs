namespace UzEx.Analytics.Application.Contracts.GetContractFromNewSpot;

public sealed class GetContractFromNewSpotResponse
{
    public long Id { get; set; }
    
    public long? ProductId { get; set; }
    
    public string? Number { get; set; }
    
    public string? Name { get; set; }
    
    public int TradeType { get; set; }

    public int Type { get; set; }
    
    public int Form { get; set; }
    
    public decimal Lot { get; set; }

    public string? Unit { get; set; }

    public decimal BasePrice { get; set; }
    
    public string? Currency { get; set; }
    
    public string? DeliveryBase { get; set; }
    
    public string? Warehouse { get; set; }
    
    public string? OriginCOuntry { get; set; }
}