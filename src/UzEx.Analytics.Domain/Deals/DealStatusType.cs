namespace UzEx.Analytics.Domain.Deals
{
    public enum DealStatusType
    {
        Undefined = 1,
        New = 2,
        NotSigned = 3,
        WaitingPayment = 4,
        PaymentExpired = 5,
        WaitingDelivery = 6,
        DeliveryExpired = 7,
        NotPaid = 8,
        NotDelivered = 9,
        Completed = 10,
        PartialCompleted = 11,
        Concluded = 12,
        NotRegistered = 13
    }
}
