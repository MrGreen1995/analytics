namespace UzEx.Analytics.Domain.Orders;

public enum OrderStatus
{
    New,
    Accepted,
    Rejected,
    Cancelled,
    Blocked,
    OutOfGlass,
    PartialSatisfied,
    Satisfied,
    Updated,
    Deleted,
    UnBlocked
}