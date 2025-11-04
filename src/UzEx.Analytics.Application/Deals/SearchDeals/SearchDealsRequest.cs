using UzEx.Analytics.Application.Models.Pagination;
using UzEx.Analytics.Application.Models.Shared;

namespace UzEx.Analytics.Application.Deals.SearchDeals
{
    public sealed record SearchDealsRequest : PaginationAndSorting
    {
        // Filter Parameters
        public DateOnly? From { get; init; }
        public DateOnly? To { get; init; }
        public List<int> SessionType { get; init; } = new List<int>();
        public List<int> Status { get; init; } = new List<int>();
    }
}
