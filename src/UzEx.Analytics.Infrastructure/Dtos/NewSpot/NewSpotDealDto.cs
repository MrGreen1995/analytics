using System.Text.Json.Serialization;

namespace UzEx.Analytics.Infrastructure.Dtos.NewSpot;

public sealed class NewSpotDealDto
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("modifiedDate")]
    public DateTime ModifiedDate { get; init; }

    [JsonPropertyName("number")]
    public long Number { get; init; }

    [JsonPropertyName("dealDate")]
    public DateTime DealDate { get; init; }

    [JsonPropertyName("contractId")]
    public long ContractId { get; init; }

    [JsonPropertyName("sellerClientId")]
    public required string SellerClientId { get; init; }
    
    [JsonPropertyName("sellerBrokerId")]
    public required string SellerBrokerId { get; init; }
    
    [JsonPropertyName("buyerClientId")]
    public required string BuyerClientId { get; init; }
    
    [JsonPropertyName("buyerBrokerId")]
    public required string BuyerBrokerId { get; init; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; init; }
    
    [JsonPropertyName("price")]
    public decimal Price { get; init; }

    [JsonPropertyName("priceCurrency")]
    public required string PriceCurrency { get; init; }

    [JsonPropertyName("cost")]
    public decimal Cost { get; init; }

    [JsonPropertyName("costCurrency")]
    public required string CostCurrency { get; init; }

    [JsonPropertyName("status")]
    public int Status { get; init; }

    [JsonPropertyName("paymentDays")]
    public int PaymentDays { get; init; }

    [JsonPropertyName("deliveryDays")]
    public int DeliveryDays { get; init; }

    [JsonPropertyName("paymentDate")]
    public DateTime? PaymentDate { get; init; }

    [JsonPropertyName("closeDate")]
    public DateTime? CloseDate { get; init; }

    [JsonPropertyName("sellerTradeCommissionSum")]
    public decimal SellerTradeCommissionSum { get; init; }

    [JsonPropertyName("sellerTradeCommissionCurrency")]
    public required string SellerTradeCommissionCurrency { get; init; }
    
    [JsonPropertyName("sellerClearingCommissionSum")]
    public decimal SellerClearingCommissionSum { get; init; }

    [JsonPropertyName("sellerClearingCommissionCurrency")]
    public required string SellerClearingCommissionCurrency { get; init; }

    [JsonPropertyName("sellerPledgeSum")]
    public decimal SellerPledgeSum { get; init; }
    
    [JsonPropertyName("sellerPledgeCurrency")]
    public decimal SellerPledgeCurrency { get; init; }
    
    [JsonPropertyName("buyerTradeCommissionSum")]
    public decimal BuyerTradeCommissionSum { get; init; }

    [JsonPropertyName("buyerTradeCommissionCurrency")]
    public required string  BuyerTradeCommissionCurrency { get; init; }
    
    [JsonPropertyName("buyerClearingCommissionSum")]
    public decimal BuyerClearingCommissionSum { get; init; }

    [JsonPropertyName("buyerClearingCommissionCurrency")]
    public required string BuyerClearingCommissionCurrency { get; init; }

    [JsonPropertyName("buyerPledgeSum")]
    public decimal BuyerPledgeSum { get; init; }
    
    [JsonPropertyName("buyerPledgeCurrency")]
    public decimal BuyerPledgeCurrency { get; init; }
}