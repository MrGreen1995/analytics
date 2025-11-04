using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Brokers;
using UzEx.Analytics.Domain.Calendars;
using UzEx.Analytics.Domain.Clients;
using UzEx.Analytics.Domain.Contracts;
using UzEx.Analytics.Domain.Deals.Events;
using UzEx.Analytics.Domain.Shared;

namespace UzEx.Analytics.Domain.Deals
{
    public sealed class Deal : Entity
    {
        public DateTime CreatedOnUtc { get; private set; }

        public DateTime? ModifiedOnUtc { get; private set; }

        public BusinessKey BusinessKey { get; private set; }

        public Guid CalendarId { get; private set; }

        public Calendar? Calendar { get; init; }

        public Guid ContractId { get; private set; }

        public Contract? Contract { get; init; }

        public Guid SellerClientId { get; private set; }

        public Client? SellerClient { get; init; }

        public Guid SellerBrokerId { get; private set; }

        public Broker? SellerBroker { get; init; }

        public Guid BuyerClientId { get; private set; }

        public Client? BuyerClient { get; init; }

        public Guid BuyerBrokerId { get; private set; }

        public Broker? BuyerBroker { get; init; }

        public DateTime DateOnUtc { get; private set; }

        public DealNumber Number { get; private set; }

        public DealSessionType SessionType { get; private set; }

        public DealAmount Amount { get; private set; }

        public DealPrice Price { get; private set; }

        public Money Cost { get; private set; }

        public DealStatusType Status { get; private set; }

        public DealPaymentDays PaymentDays { get; private set; }

        public DealDeliveryDays DeliveryDays { get; private set; }

        public DateTime? PaymentDate { get; private set; }

        public DateTime? CloseDate { get; private set; }

        public DateTime? AnnulDate { get; private set; }

        public string? AnnulReason { get; private set; }

        public Money SellerTradeCommission { get; private set; }
        
        public Money SellerClearingCommission { get; private set; }
        
        public Money SellerPledge { get; private set; }
        
        public Money BuyerTradeCommission { get; private set; }
        
        public Money BuyerClearingCommission { get; private set; }
        
        public Money BuyerPledge { get; private set; }
        
        private Deal()
        {
        }

        public Deal(
            Guid id,
            DateTime createdOnUtc,
            BusinessKey businessKey,
            Guid calendarId,
            Guid contractId,
            Guid sellerClientId,
            Guid sellerBrokerId,
            Guid buyerClientId,
            Guid buyerBrokerId,
            DateTime dateOnUtc,
            DealNumber number,
            DealSessionType sessionType,
            DealAmount amount,
            DealPrice price,
            Money cost,
            DealStatusType status,
            DealPaymentDays paymentDays,
            DealDeliveryDays deliveryDays,
            Money sellerTradeCommission,
            Money sellerClearingCommission,
            Money sellerPledge,
            Money buyerTradeCommission,
            Money buyerClearingCommission,
            Money buyerPledge) : base(id)
        {
            CreatedOnUtc = createdOnUtc;
            BusinessKey = businessKey;
            CalendarId = calendarId;
            ContractId = contractId;
            SellerClientId = sellerClientId;
            SellerBrokerId = sellerBrokerId;
            BuyerClientId = buyerClientId;
            BuyerBrokerId = buyerBrokerId;
            DateOnUtc = dateOnUtc;
            Number = number;
            SessionType = sessionType;
            Amount = amount;
            Price = price;
            Cost = cost;
            Status = status;
            PaymentDays = paymentDays;
            DeliveryDays = deliveryDays;
            SellerTradeCommission = sellerTradeCommission;
            SellerClearingCommission = sellerClearingCommission;
            SellerPledge = sellerPledge;
            BuyerTradeCommission = buyerTradeCommission;
            BuyerClearingCommission = buyerClearingCommission;
            BuyerPledge = buyerPledge;
        }

        public static Deal Create(
            string businessKey,
            DateTime createdOnDate,
            Guid calendarId,
            Guid contractId,
            Guid sellerClientId,
            Guid sellerBrokerId,
            Guid buyerClientId,
            Guid buyerBrokerId,
            DateTime dateOnUtc,
            string number,
            decimal amount,
            decimal price,
            Money cost,
            int paymentDays,
            int deliveryDays,
            DealStatusType status,
            int sessionType,
            Money sellerTradeCommission,
            Money sellerClearingCommission,
            Money sellerPledge,
            Money buyerTradeCommission,
            Money buyerClearingCommission,
            Money buyerPledge)
        {
            var deal = new Deal(
                Guid.NewGuid(),
                createdOnDate,
                new BusinessKey(businessKey),
                calendarId,
                contractId,
                sellerClientId,
                sellerBrokerId,
                buyerClientId,
                buyerBrokerId,
                dateOnUtc,
                new DealNumber(number),
                (DealSessionType)sessionType,
                new DealAmount(amount),
                new DealPrice(price),
                cost,
                status,
                new DealPaymentDays(paymentDays),
                new DealDeliveryDays(deliveryDays),
                sellerTradeCommission,
                sellerClearingCommission,
                sellerPledge,
                buyerTradeCommission,
                buyerClearingCommission,
                buyerPledge);

            deal.RaiseDomainEvent(new DealCreatedDomainEvent(deal.Id));

            return deal;
        }
        
        public Result SetCloseDate(DateTime date)
        {
            CloseDate = date.ToUniversalTime();
            return Result.Success();
        }
        
        public Result SetPaymentDate(DateTime date)
        {
            PaymentDate = date.ToUniversalTime();
            return Result.Success();
        }
        
        public Result SetAnnulDate(DateTime date)
        {
            AnnulDate = date.ToUniversalTime();
            return Result.Success();
        }
        
        public Result SetAnnulReason(string reason)
        {
            AnnulReason = reason.Trim().ToUpper();
            return Result.Success();
        }
    }
}
