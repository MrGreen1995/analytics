using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Brokers;
using UzEx.Analytics.Domain.Calendars;
using UzEx.Analytics.Domain.Clients;
using UzEx.Analytics.Domain.Contracts;
using UzEx.Analytics.Domain.Orders.Events;
using UzEx.Analytics.Domain.Shared;

namespace UzEx.Analytics.Domain.Orders;

public sealed class Order : Entity
{
    public BusinessKey BusinessKey { get; private set; }

    public Guid ContractId { get; private set; }

    public Contract Contract { get; init; }

    public Guid CalendarId { get; private set; }

    public Calendar Calendar { get; init; }

    public Guid ClientId { get; private set; }

    public Client Client { get; init; }
    
    public Guid BrokerId { get; private set; }
    
    public Broker Broker { get; init; }

    public OrderDirectionType Direction { get; private set; }

    public OrderStatus Status { get; private set; }

    public DateTime ReceiveDate { get; private set; }
    
    public OrderAmount Amount { get; private set; }
    
    public OrderPrice Price { get; private set; }
    
    public string? ParentId { get; private set; }

    private Order()
    {
    }

    public Order(
        Guid id,
        BusinessKey businessKey, 
        Guid calendarId,
        Guid contractId, 
        Guid clientId,
        Guid brokerId,
        OrderDirectionType direction,
        OrderStatus status,
        DateTime receiveDate, 
        OrderAmount amount,
        OrderPrice price) : base(id)
    {
        BusinessKey = businessKey;
        CalendarId = calendarId;
        ContractId = contractId;
        ClientId = clientId;
        BrokerId = brokerId;
        ReceiveDate = receiveDate;
        Amount = amount;
        Price = price;
        Direction = direction;
        Status = status;
    }

    public static Order Create(
        Guid id,
        string businessKey,
        Guid calendarId,
        Guid contractId,
        Guid clientId,
        Guid brokerId,
        OrderDirectionType direction,
        OrderStatus status,
        DateTime receiveDate,
        decimal amount,
        decimal price)
    {
        var order = new Order(
            id, 
            new BusinessKey(businessKey), 
            calendarId,
            contractId, 
            clientId, 
            brokerId,
            direction,
            status,
            receiveDate, 
            new OrderAmount(amount), 
            new OrderPrice(price));
        
        order.RaiseDomainEvent(new OrderCreatedDomainEvent(order.Id));
        
        return order;
    }
    
    public Result SetParent(string key)
    {
        ParentId = key;

        return Result.Success();
    }
}