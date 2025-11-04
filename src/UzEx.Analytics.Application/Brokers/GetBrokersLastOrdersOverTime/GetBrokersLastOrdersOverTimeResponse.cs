namespace UzEx.Analytics.Application.Brokers.GetBrokersLastOrdersOverTime;

public sealed class GetBrokersLastOrdersOverTimeResponse
{
    public Guid Id { get; init; }

    public string? Direction { get; init; }

    public string? ClientTin { get; init; }

    public string? ClientName { get; init; }

    public string? ContractNumber { get; init; }

    public string? ContractName { get; init; }

    public decimal Amount { get; init; }

    public decimal Price { get; init; }

    public decimal? ContractLot { get; init; }

    public string? ContractUnit { get; init; }

    public string? ContractCurrency { get; init; }

    public decimal? Volume => ContractLot * Amount;
}
