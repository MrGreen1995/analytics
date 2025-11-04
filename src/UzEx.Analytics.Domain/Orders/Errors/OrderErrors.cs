using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Domain.Orders.Errors;

public class OrderErrors
{
    public static Error NotFound = new ("Order.Found", "Order not found");
}