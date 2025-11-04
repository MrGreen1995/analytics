using UzEx.Analytics.Domain.Clients;

namespace UzEx.Analytics.Application.Clients.SearchClients
{
    public sealed record SearchClientsResponse
    {
        public Guid Id { get; init; }
        public DateTime CreatedOnUtc { get; init; }

        public ClientType Type { get; init; }

        public string? RegNumber { get; init; }

        public string? Name { get; init; }

        public string? Country { get; init; }

        public string? Region { get; init; }

        public string? District { get; init; }

        public string? Address { get; init; }
    }
}
