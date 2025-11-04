namespace UzEx.Analytics.Application.NewSpot.GetClient;

public sealed class GetClientFromNewSpotResponse
{
    public required string Id { get; init; }

    public int AccountType { get; init; }

    public required string Tin { get; init; }

    public required string Name { get; init; }

    public string? CountryCode { get; init; }

    public string? RegionCode { get; init; }

    public string? DistrictCode { get; init; }

    public string? Address { get; init; }
}
