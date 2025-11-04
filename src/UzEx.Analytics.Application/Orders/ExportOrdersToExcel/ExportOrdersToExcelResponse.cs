namespace UzEx.Analytics.Application.Orders.ExportOrdersToExcel;

public sealed class ExportOrdersToExcelResponse
{
    public string? Direction { get; init; }

    public decimal Amount { get; init; }

    public decimal Price { get; init; }

    public DateTime ReceiveDate { get; init; }

    public string? Status { get; init; }

    public string? BrokerName { get; init; }

    public string? BrokerNumber { get; init; }

    public string? ClientTin { get; init; }

    public string? ClientName { get; init; }

    public string? ContractNumber { get; init; }

    public string? ContractName { get; init; }

    public decimal? ContractLot { get; init; }

    public string? ContractUnit { get; init; }

    public string? ContractCurrency { get; init; }

    public decimal? Volume => ContractLot * Amount;
}
