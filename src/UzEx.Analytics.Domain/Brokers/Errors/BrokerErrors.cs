using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Domain.Brokers.Errors;

public sealed class BrokerErrors
{
    public static Error NotFound = new ("Broker.Found", "Broker not found");
}