using UzEx.Analytics.Domain.Deals;

namespace UzEx.Analytics.Application.Deals.SearchDeals
{
    public sealed class SearchDealsResponse
    {
        public Guid Id { get; init; }
        public DateTime CreatedOnUtc { get; init; }

        public DateTime? ModifiedOnUtc { get; init; }

        public string? BusinessKey { get; init; }

        public DateTime DateOnUtc { get; init; }

        public string? Number { get; init; }

        public DealSessionType SessionType { get; init; }

        public decimal Amount { get; init; }

        public decimal Price { get; init; }

        public decimal Cost { get; init; }

        public DealStatusType Status { get; init; }

        public int PaymentDays { get; init; }

        public int DeliveryDays { get; init; }

        public DateTime? PaymentDate { get; init; }

        public DateTime? CloseDate { get; init; }

        public DateTime? AnnulDate { get; init; }

        public string? AnnulReason { get; init; }
    }
}
