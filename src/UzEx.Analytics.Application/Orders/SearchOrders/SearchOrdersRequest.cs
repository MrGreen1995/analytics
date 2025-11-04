using UzEx.Analytics.Application.Models.Shared;

namespace UzEx.Analytics.Application.Orders.SearchOrders
{
    public sealed record SearchOrdersRequest : PaginationAndSorting
    {
        // Filter Parameters
        public DateOnly? DateFrom { get; init; }
        public DateOnly? DateTo { get; init; }
        public List<int> Direction { get; init; } = new List<int>();
    }
}
 