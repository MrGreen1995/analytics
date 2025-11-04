namespace UzEx.Analytics.Application.Brokers.BrokerParticipatedDeals.Shared;

public sealed class GetBrokerHandledDealsCostOverTimeRequest
{
    public Guid Id { get; init; }
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
}
