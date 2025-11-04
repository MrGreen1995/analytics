using UzEx.Analytics.Application.Models.Shared;

namespace UzEx.Analytics.Application.Clients.SearchClients
{
    public sealed record SearchClientsRequest : PaginationAndSorting
    {
        public DateOnly? From { get; init; }
        public DateOnly? To { get; init; }

        public List<int> Type { get; init; } = new List<int>();

        public string? RegNumber { get; init; }

        public string? Name { get; init; }

        public string? Country { get; init; }

        public string? Region { get; init; }

        public string? District { get; init; }

        public string? Address { get; init; }
    }
}
