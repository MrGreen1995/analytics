namespace UzEx.Analytics.Application.Dashboards.GetExchangeVolumes;

public sealed class GetExchangeVolumesResponse
{
    public required string Trade { get; init; }

    public decimal Volume { get; init; }

    public required string VolumeUnit{ get; init; }
}