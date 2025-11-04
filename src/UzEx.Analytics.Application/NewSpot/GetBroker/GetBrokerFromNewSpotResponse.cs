namespace UzEx.Analytics.Application.NewSpot.GetBroker;

public sealed class GetBrokerFromNewSpotResponse
{
    public required string Id { get; init; }

    public int AccountType { get; init; }

    public required string Tin { get; init; }

    public required string BrokerNumber { get; init; }

    public required string Name { get; init; }

    public string? CountryCode { get; init; }

    public string? RegionCode { get; init; }

    public string? DistrictCode { get; init; }

    public string? Address { get; init; }
}
