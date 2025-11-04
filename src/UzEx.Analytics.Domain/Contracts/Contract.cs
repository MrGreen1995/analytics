using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Contracts.Events;
using UzEx.Analytics.Domain.Deals;
using UzEx.Analytics.Domain.Orders;
using UzEx.Analytics.Domain.Products;
using UzEx.Analytics.Domain.Shared;

namespace UzEx.Analytics.Domain.Contracts;

public sealed class Contract : Entity
{
    public DateTime CreatedOnUtc { get; private set; }

    public BusinessKey BusinessKey { get; private set; }

    public Guid? ProductId { get; private set; }
    
    public Product? Product { get; init; }
    
    public ContractNumber Number { get; private set; }
    
    public ContractName Name { get; private set; }

    public ContractPlatformType Platform { get; set; }

    public ContractType Type { get; private set; }
    
    public ContractForm Form { get; private set; }

    public ContractTradeType TradeType { get; private set; }

    public ContractLot Lot { get; private set; }
    
    public ContractUnit Unit { get; private set; }
    
    public ContractBasePrice BasePrice { get; private set; }
    
    public ContractCurrency Currency { get; private set; }
    
    public ContractDeliveryBase DeliveryBase { get; private set; }
    
    public ContractWarehouse Warehouse { get; private set; }

    public ContractOriginCountry OriginCountry { get; private set; }
    
    public ICollection<Order>? Orders { get; set; }

    public ICollection<Deal>? Deals { get; set; }

    private Contract()
    {
    }

    public Contract(
        Guid id, 
        DateTime createdOnUtc,
        BusinessKey businessKey, 
        ContractNumber number, 
        ContractName name,
        ContractPlatformType platform,
        ContractType type,
        ContractForm form,
        ContractTradeType tradeType,
        ContractLot lot,
        ContractUnit unit,
        ContractBasePrice basePrice,
        ContractCurrency currency,
        ContractDeliveryBase deliveryBaseBase,
        ContractWarehouse warehouse,
        ContractOriginCountry originCountry) : base(id)
    {
        CreatedOnUtc = createdOnUtc;
        BusinessKey = businessKey;
        Number = number;
        Name = name;
        Platform = platform;
        Type = type;
        Form = form;
        TradeType = tradeType;
        Lot = lot;
        Unit = unit;
        BasePrice = basePrice;
        Currency = currency;
        DeliveryBase = deliveryBaseBase;
        Warehouse = warehouse;
        OriginCountry = originCountry;
    }

    public static Contract Create(
        Guid id,
        DateTime createdOnUtc,
        string businessKey,
        string number,
        string name,
        ContractPlatformType platform,
        ContractType type,
        ContractForm form,
        ContractTradeType tradeType,
        decimal lot,
        string unit,
        decimal basePrice,
        string currency,
        string deliveryBaseBase,
        string warehouse,
        string originCountry)
    {
        var contract = new Contract(
            id,
            createdOnUtc,
            new BusinessKey(businessKey),
            new ContractNumber(number),
            new  ContractName(name),
            platform,
            type,
            form,
            tradeType,
            new ContractLot(lot),
            new ContractUnit(unit),
            new ContractBasePrice(basePrice),
            new ContractCurrency(currency),
            new ContractDeliveryBase(deliveryBaseBase),
            new ContractWarehouse(warehouse),
            new ContractOriginCountry(originCountry));

        contract.RaiseDomainEvent(new ContractCreatedDomainEvent(id));

        return contract;
    }
    
    public Result SetProductId(Guid id)
    {
        ProductId = id;

        return Result.Success();
    }
}