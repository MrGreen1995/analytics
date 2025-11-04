using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Brokers.Events;
using UzEx.Analytics.Domain.Deals;
using UzEx.Analytics.Domain.Orders;
using UzEx.Analytics.Domain.Shared;

namespace UzEx.Analytics.Domain.Brokers;

public sealed class Broker : Entity
{
    public DateTime CreatedOnUtc { get; private set; }

    public BusinessKey BusinessKey { get; set; }
    
    public BrokerRegNumber RegNumber { get; private set; }
    
    public BrokerName Name { get; private set; }
    
    public BrokerNumber Number { get; private set; }
    
    public BrokerRegion Region { get; private set; }
    
    public BrokerNewSpotKey NewSpotKey { get; private set; }
    
    public BrokerOldSpotKey OldSpotKey { get; private set; }
    
    public IReadOnlyCollection<Order>? Orders { get; private set; }
    
    public IReadOnlyCollection<Deal>? SellerBrokerDeals { get; set; }
        
    public IReadOnlyCollection<Deal>? BuyerBrokerDeals { get; set; }

    private Broker()
    {
    }

    public Broker(
        Guid id, 
        DateTime createdOnUtc,
        BusinessKey businessKey,
        BrokerRegNumber regNumber,
        BrokerName name,
        BrokerNumber number,
        BrokerRegion region) : base(id)
    {
        CreatedOnUtc = createdOnUtc;
        RegNumber = regNumber;
        Name = name;
        Number = number;
        Region = region;
        BusinessKey = businessKey;
    }

    public static Broker Create(
        Guid id,
        DateTime createdOnUtc,
        string bussinessKey,
        string regNum,
        string name,
        string number,
        string region)
    {
        var broker = new Broker(
            id,
            createdOnUtc,
            new BusinessKey(bussinessKey),
            new BrokerRegNumber(regNum),
            new BrokerName(name),
            new BrokerNumber(number),
            new BrokerRegion(region));
        
        broker.RaiseDomainEvent(new BrokerCreatedDomainEvent(broker.Id));
        
        return broker;
    }
    
    public Result SetNewSpotKey(string key)
    {
        NewSpotKey = new BrokerNewSpotKey(key);

        return Result.Success();
    }
        
    public Result SetOldSpotKey(string key)
    {
        OldSpotKey = new BrokerOldSpotKey(key);

        return Result.Success();
    }
}