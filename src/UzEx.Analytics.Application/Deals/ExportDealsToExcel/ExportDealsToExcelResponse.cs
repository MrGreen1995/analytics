namespace UzEx.Analytics.Application.Deals.ExportDealsToExcel;

public sealed class ExportDealsToExcelResponse
{
    public DateTime CreatedOnUtc { get; init; }

    public DateTime DateOnUtc { get; init; }

    public string? Number { get; init; }

    public string? ContractName { get; set; }

    public string? ContractNumber { get; set; }

    public decimal? ContractLot { get; init; }

    public string? ContractUnit { get; init; }

    public string? ContractCurrency { get; init; }

    public string? SellerClientTin { get; init; }

    public string? SellerClientName { get; set; }

    public string? BuyerClientTin { get; init; }

    public string? BuyerClientName { get; set; }

    public string? SellerBrokerName { get; init; }

    public string? SellerBrokerNumber { get; init; }

    public string? BuyerBrokerName { get; init; }

    public string? BuyerBrokerNumber { get; init; }

    public string? SessionType { get; init; }

    public decimal Amount { get; init; }

    public decimal Price { get; init; }

    public decimal Cost { get; init; }

    public string? Status { get; init; }

    public int PaymentDays { get; init; }

    public int DeliveryDays { get; init; }

    public DateTime? PaymentDate { get; init; }

    public DateTime? CloseDate { get; init; }

    public decimal SellerTradeCommission { get; init; }

    public decimal SellerClearingCommission { get; init; }

    public decimal SellerPledge { get; init; }

    public decimal BuyerTradeCommission { get; init; }

    public decimal BuyerClearingCommission { get; init; }

    public decimal BuyerPledge { get; init; }
}
