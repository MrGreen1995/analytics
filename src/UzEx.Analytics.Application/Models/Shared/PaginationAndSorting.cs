using UzEx.Analytics.Application.Models.Pagination;

namespace UzEx.Analytics.Application.Models.Shared
{
    public record PaginationAndSorting
    {
        // Pagination
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;

        // Sorting (multi-column sorting)
        public List<SortCriterion>? SortCriteria { get; init; }
    }
}
