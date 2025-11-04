using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Domain.Deals.Errors
{
    public class DealErrors
    {
        public static Error NotFound = new("Deal.Found", "Deal not found");
    }
}
