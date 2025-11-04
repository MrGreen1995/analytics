namespace UzEx.Analytics.Application.Models.NewSpot;

public sealed class NewSpotDealModel
{
    public required string Id { get; init; }

    public DateTime ModifiedDate { get; init; }

    public long Number { get; init; }

    public DateTime DealDate { get; init; }
    
    public long ContractId { get; init; }

    public required string SellerClientId { get; init; }
    
    public required string SellerBrokerId { get; init; }
    
    public required string BuyerClientId { get; init; }
    
    public required string BuyerBrokerId { get; init; }

    public decimal Amount { get; init; }
    
    public decimal Price { get; init; }

    public required string PriceCurrency { get; init; }

    public decimal Cost { get; init; }

    public required string CostCurrency { get; init; }

    public int Status { get; init; }

    public int PaymentDays { get; init; }

    public int DeliveryDays { get; init; }

    public DateTime? PaymentDate { get; init; }

    public DateTime? CloseDate { get; init; }

    public decimal SellerTradeCommissionSum { get; init; }

    public required string SellerTradeCommissionCurrency { get; init; }
    
    public decimal SellerClearingCommissionSum { get; init; }

    public required string SellerClearingCommissionCurrency { get; init; }

    public decimal SellerPledgeSum { get; init; }
    
    public decimal SellerPledgeCurrency { get; init; }
    
    public decimal BuyerTradeCommissionSum { get; init; }

    public required string  BuyerTradeCommissionCurrency { get; init; }
    
    public decimal BuyerClearingCommissionSum { get; init; }

    public required string BuyerClearingCommissionCurrency { get; init; }

    public decimal BuyerPledgeSum { get; init; }
    
    public decimal BuyerPledgeCurrency { get; init; }
}