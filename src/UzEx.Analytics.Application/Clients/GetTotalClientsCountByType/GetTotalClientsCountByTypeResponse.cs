namespace UzEx.Analytics.Application.Clients.GetTotalClientsCountByType;

public sealed class GetTotalClientsCountByTypeResponse
{
    public string ClientType { get; init; } = string.Empty;

    public int Count { get; set; }
}
