using System.Text.Json;
using UzEx.Analytics.Application.Abstractions.Clock;
using UzEx.Analytics.Application.Abstractions.HandBook;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Abstractions.NewSpot;
using UzEx.Analytics.Application.Models.NewSpot;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Brokers;
using UzEx.Analytics.Domain.Calendars;
using UzEx.Analytics.Domain.Clients;
using UzEx.Analytics.Domain.Contracts;
using UzEx.Analytics.Domain.DataMigrations;
using UzEx.Analytics.Domain.DataMigrations.Errors;
using UzEx.Analytics.Domain.Deals;
using UzEx.Analytics.Domain.Orders;
using UzEx.Analytics.Domain.Shared;

namespace UzEx.Analytics.Application.DataMigrations.ProcessDataMigrations;

public sealed class DataMigrationProcessQueryHandler : IQueryHandler<DataMigrationProcessQuery, bool>
{
    private readonly IDataMigrationRepository _dataMigrationrepository;
    private readonly IClientRepository _clientrepository;
    private readonly IBrokerRepository _brokerrepository;
    private readonly IContractRepository _contractrepository;
    private readonly IDealRepository _dealrepository;
    private readonly INewSpotService _newspotservice;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderRepository _orderRepository;
    private readonly ICalendarRepository _calendarRepository;
    private readonly IHandBookService _handBookService;
    
    public DataMigrationProcessQueryHandler(
        IDataMigrationRepository dataMigrationrepository, 
        IClientRepository clientrepository, 
        IBrokerRepository brokerrepository, 
        IContractRepository contractrepository, 
        IDealRepository dealrepository,
        INewSpotService newSpotService, 
        IDateTimeProvider dateTimeProvider,
        IUnitOfWork unitOfWork, 
        IOrderRepository orderRepository, 
        ICalendarRepository calendarRepository, 
        IHandBookService handBookService)
    {
        _dataMigrationrepository = dataMigrationrepository;
        _clientrepository = clientrepository;
        _brokerrepository = brokerrepository;
        _contractrepository = contractrepository;
        _dealrepository = dealrepository;
        _newspotservice = newSpotService;
        _dateTimeProvider = dateTimeProvider;
        _unitOfWork = unitOfWork;
        _orderRepository = orderRepository;
        _calendarRepository = calendarRepository;
        _handBookService = handBookService;
    }
    
    private async Task<(bool, string)> ProcessNewSpOrder(NewSpotOrderModel orderModel, CancellationToken cancellationToken)
    {
        var orderDateKey = orderModel.Date.Year * 10000 + orderModel.Date.Month * 100 + orderModel.Date.Day;
        
        var calendar = await _calendarRepository.GetByDateKeyAsync(orderDateKey, cancellationToken);
        if (calendar == null)
        {
            calendar = Calendar.Create(
                Guid.NewGuid(), 
                new DateOnly(orderModel.Date.Year, orderModel.Date.Month, orderModel.Date.Day),
                _dateTimeProvider.UtcNow,
                orderDateKey);
            
            _calendarRepository.Add(calendar);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        
        var client = await _clientrepository.GetByNewSpotKeyAsync(orderModel.ClientId, cancellationToken);
        if (client == null)
        {
            var clientModel = await _newspotservice.GetClient(orderModel.ClientId, cancellationToken);
            if (clientModel == null)
            {
                return (false, $"Client id: {orderModel.ClientId} not found");
            }
            
            var accountType = clientModel.AccountType switch
            {
                0 => ClientType.Yatt,
                1 => ClientType.Rezident,
                2 => ClientType.Physical,
                3 => ClientType.NonRezident,
                4 => ClientType.Branch,
                _ => ClientType.Undefined
            };

            var countryCode = clientModel.CountryCode ?? "1";
            var countryFromHb = await _handBookService.GetCountryByIdAsync(countryCode, cancellationToken);

            if (countryFromHb == null)
            {
                return (false, $"Country id: {orderModel.ClientId} not found");
            }
            
            var regionCode = clientModel.RegionCode ?? "";
            var regionFromHb = await _handBookService.GetUzbRegionByIdAsync(clientModel.RegionCode, cancellationToken);
            
            //var districtCode = clientModel.DistrictCode ?? "";
            //var districtFromHb = await _handBookService.GetDistrictById(regionFromHb?.Id.ToString(), districtCode, cancellationToken);
            
            client = Client.Create(
                Guid.NewGuid(), 
                _dateTimeProvider.UtcNow,
                (int)accountType,
                clientModel.Tin.Trim(),
                clientModel.Name,
                countryFromHb.Name.Trim(),
                regionFromHb?.Name ?? "NOT AVAILABLE",
                "NOT AVAILABLE",
                clientModel.Address ?? "NOT AVAILABLE");
            
            client.SetNewSpotKey(clientModel.Id);
            client.SetOldSpotKey("---");
            
            _clientrepository.Add(client);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        
        var broker = await _brokerrepository.GetByNewSpotKeyAsync(orderModel.BrokerId, cancellationToken);
        if (broker == null)
        {
            var brokerModel = await _newspotservice.GetBroker(orderModel.BrokerId, cancellationToken);
            if (brokerModel == null)
            {
                return (false, $"Broker id: {orderModel.BrokerId} not found");
            }
            
            broker = Broker.Create(
                Guid.NewGuid(), 
                _dateTimeProvider.UtcNow,
                brokerModel.Id,
                brokerModel.Tin,
                brokerModel.Name,
                brokerModel.BrokerNumber,
                brokerModel.RegionCode ?? "NOT AVAILABLE");
            
            broker.SetNewSpotKey(brokerModel.Id);
            broker.SetOldSpotKey("xxx");
            
            _brokerrepository.Add(broker);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        
        var contract = await _contractrepository.GetByBusinessKeyAsync(
            orderModel.ContractId.ToString(), 
            ContractPlatformType.SpTwo, 
            cancellationToken);

        if (contract == null)
        {
            var contractModel = await _newspotservice.GetContract(orderModel.ContractId, cancellationToken);
            if (contractModel == null)
            {
                return (false, $"Contract id: {orderModel.ContractId} not found");
            }
            
            var contractTradeType = contractModel.TradeType switch
            {
                0 => ContractTradeType.EnglishAuction,
                1 => ContractTradeType.DeutchAuction,
                2 => ContractTradeType.DoubleAuction,
                3 => ContractTradeType.ReverseAuction,
                _ => ContractTradeType.Undefined
            };
            
            var contractType = contractModel.Type switch
            {
                0 => ContractType.Internal,
                1 => ContractType.Export,
                2 => ContractType.Import,
                _ => ContractType.Undefined
            };
            
            var contractForm = contractModel.Form switch
            {
                0 => ContractForm.Spot,
                1 => ContractForm.Forward,
                2 => ContractForm.Futures,
                _ => ContractForm.Undefined
            };
            
            contract = Contract.Create(
                Guid.NewGuid(), 
                _dateTimeProvider.UtcNow,
                contractModel.Id.ToString(),
                contractModel.Number.Trim(),
                contractModel.Name.Trim(),
                ContractPlatformType.SpTwo,
                contractType,
                contractForm,
                contractTradeType,
                contractModel.Lot,
                contractModel.Unit.Trim(),
                contractModel.BasePrice,
                contractModel.Currency.Trim(),
                contractModel.DeliveryBase,
                contractModel.Warehouse.Trim(),
                contractModel.OriginCountryId.Trim());
            
            _contractrepository.Add(contract);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        
        var order = await _orderRepository.GetByBusinessKeyAsync(orderModel.Id, cancellationToken);
        if (order != null)
        {
            return (false, $"Order id: {orderModel.Id} exists");
        }
        else
        {
            order = Order.Create(
                Guid.NewGuid(),
                orderModel.Id,
                calendar.Id,
                contract.Id,
                client.Id,
                broker.Id,
                (OrderDirectionType)orderModel.Direction,
                (OrderStatus)orderModel.Status,
                orderModel.Date.ToUniversalTime(),
                orderModel.Amount,
                orderModel.Price);

            if (orderModel.ParentId != null)
            {
                order.SetParent(orderModel.ParentId);
            }
            
            _orderRepository.Add(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        
        return (true, string.Empty);
    }

    private async Task<(bool, string)> ProcessNewSpDeal(NewSpotDealModel dealModel, CancellationToken cancellationToken)
    {
        var dateKey = dealModel.DealDate.Year * 10000 + dealModel.DealDate.Month * 100 + dealModel.DealDate.Day;
        
        var calendar = await _calendarRepository.GetByDateKeyAsync(dateKey, cancellationToken);
        if (calendar == null)
        {
            var dateOnly = new DateOnly(dealModel.DealDate.Year, dealModel.DealDate.Month, dealModel.DealDate.Day);
            
            calendar = Calendar.Create(Guid.NewGuid(), dateOnly, _dateTimeProvider.UtcNow, dateKey);
            
            _calendarRepository.Add(calendar);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        
        var contract = await _contractrepository.GetByBusinessKeyAsync(
            dealModel.ContractId.ToString(), 
            ContractPlatformType.SpTwo, 
            cancellationToken);

        if (contract == null)
        {
            return (false, $"contract not {dealModel.ContractId} not found");
        }
        
        var sellerClient = await _clientrepository.GetByNewSpotKeyAsync(dealModel.SellerClientId, cancellationToken);
        if (sellerClient == null)
        {
            return (false, $"seller client not {dealModel.SellerClientId} not found");
        }
        
        var sellerBroker = await _brokerrepository.GetByNewSpotKeyAsync(dealModel.SellerBrokerId, cancellationToken);
        if (sellerBroker == null)
        {
            return (false, $"seller broker not {dealModel.SellerBrokerId} not found");
        }
        
        var buyerClient = await _clientrepository.GetByNewSpotKeyAsync(dealModel.BuyerClientId, cancellationToken);
        if (buyerClient == null)
        {
            return (false, $"buyer client not {dealModel.BuyerClientId} not found");
        }
        
        var buyerBroker = await _brokerrepository.GetByNewSpotKeyAsync(dealModel.BuyerBrokerId, cancellationToken);
        if (buyerBroker == null)
        {
            return (false, $"buyer broker not {dealModel.BuyerBrokerId} not found");
        }

        var deal = await _dealrepository.GetByBusinessKey(dealModel.Id, cancellationToken);
        if (deal != null)
        {
            return (false, $"Deal id: {deal.Id} bKey: {deal.BusinessKey.Value} exists!!!");
        }

        var sessionType = dealModel.DealDate.Hour < 14 ? 2 : 3;
        
        var dealCost = new Money(dealModel.Cost, Currency.FromCode("UZS"));
        
        var sellerTradeCommission = new Money(dealModel.SellerTradeCommissionSum, Currency.FromCode("UZS"));
        var sellerClearingCommission = new Money(dealModel.SellerClearingCommissionSum, Currency.FromCode("UZS"));
        var sellerPledge = new Money(dealModel.SellerPledgeSum, Currency.FromCode("UZS"));
        
        var buyerTradeCommission = new Money(dealModel.BuyerTradeCommissionSum, Currency.FromCode("UZS"));
        var buyerClearingCommission = new Money(dealModel.BuyerClearingCommissionSum, Currency.FromCode("UZS"));
        var buyerPledge = new Money(dealModel.BuyerPledgeSum, Currency.FromCode("UZS"));

        var dealStatus = dealModel.Status switch
        {
            0 => DealStatusType.New,
            1 => DealStatusType.NotSigned,
            2 => DealStatusType.WaitingPayment,
            3 => DealStatusType.PaymentExpired,
            4 => DealStatusType.WaitingDelivery,
            5 => DealStatusType.DeliveryExpired,
            6 => DealStatusType.NotPaid,
            7 => DealStatusType.NotDelivered,
            8 => DealStatusType.Completed,
            9 => DealStatusType.PartialCompleted,
            10 => DealStatusType.Concluded,
            11 => DealStatusType.NotRegistered,
            _ => DealStatusType.Undefined
        };
        
        deal = Deal.Create(
            dealModel.Id,
            _dateTimeProvider.UtcNow,
            calendar.Id,
            contract.Id,
            sellerClient.Id,
            sellerBroker.Id,
            buyerClient.Id,
            buyerBroker.Id,
            dealModel.DealDate.ToUniversalTime(),
            dealModel.Number.ToString(),
            dealModel.Amount,
            dealModel.Price,
            dealCost,
            dealModel.PaymentDays,
            dealModel.DeliveryDays,
            dealStatus,
            sessionType,
            sellerTradeCommission,
            sellerClearingCommission,
            sellerPledge,
            buyerTradeCommission,
            buyerClearingCommission,
            buyerPledge);

        if (dealModel.PaymentDate.HasValue && dealModel.PaymentDate.Value != DateTime.MinValue)
        {
            deal.SetPaymentDate(dealModel.PaymentDate.Value);
        }

        if (deal.Status is DealStatusType.Completed or DealStatusType.PartialCompleted)
        {
            deal.SetCloseDate(dealModel.ModifiedDate);
        }

        if (deal.Status is DealStatusType.NotPaid or DealStatusType.NotDelivered or DealStatusType.NotRegistered)
        {
            if (dealModel.CloseDate.HasValue)
            {
                deal.SetAnnulDate(dealModel.CloseDate.Value);
                deal.SetAnnulReason(dealModel.Status.ToString().Trim().ToUpper());
            }
        }
        
        _dealrepository.Add(deal);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return (true, string.Empty);
    }
    
    private async Task<(bool, string)> ProcessOldSpOrder(NewSpotOrderModel orderModel, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return (true, string.Empty);
    }
    
    private async Task<(bool, string)> ProcessOldSpDeal(NewSpotDealModel dealModel, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return (true, string.Empty);
    }
    
    public async Task<Result<bool>> Handle(DataMigrationProcessQuery request, CancellationToken cancellationToken)
    {
        var dataMigration = await _dataMigrationrepository.GetByIdAsync(request.Id, cancellationToken);

        if (dataMigration == null)
        {
            return Result.Failure<bool>(DataMigrationErrors.NotFound);
        }

        switch (dataMigration.PlatformType)
        {
            case DataMigrationPlatformType.OldSp:
            {
                switch (dataMigration.DataType)
                {
                    case DataMigrationDataType.Order:
                    {
                        var orderModel = JsonSerializer.Deserialize<NewSpotOrderModel>(dataMigration.Payload.Value);

                        if (orderModel == null)
                        {
                            return Result.Failure<bool>(DataMigrationErrors.NotFound);
                        }

                        var (ok, msg) = await ProcessOldSpOrder(orderModel, cancellationToken);
                        break;
                    }
                    case DataMigrationDataType.Deal:
                    {
                        var dealModel = JsonSerializer.Deserialize<NewSpotDealModel>(dataMigration.Payload.Value);

                        if (dealModel == null)
                        {
                            return Result.Failure<bool>(DataMigrationErrors.NotFound);
                        }
                        
                        var (ok, msg) = await ProcessOldSpDeal(dealModel, cancellationToken);
                        break;
                    }
                    default:
                    {
                        return Result.Failure<bool>(DataMigrationErrors.NotFound); 
                    }
                }
                break;
            }
            case DataMigrationPlatformType.NewSp:
            {
                switch (dataMigration.DataType)
                {
                    case DataMigrationDataType.Order:
                    {
                        var orderModel = JsonSerializer.Deserialize<NewSpotOrderModel>(dataMigration.Payload.Value);

                        if (orderModel == null)
                        {
                            return Result.Failure<bool>(DataMigrationErrors.NotFound);
                        }
                        
                        var (ok, msg) = await ProcessNewSpOrder(orderModel, cancellationToken);
                        break;
                    }
                    case DataMigrationDataType.Deal:
                    {
                        var dealModel = JsonSerializer.Deserialize<NewSpotDealModel>(dataMigration.Payload.Value);

                        if (dealModel == null)
                        {
                            return Result.Failure<bool>(DataMigrationErrors.NotFound);
                        }
                        
                        var (ok, msg) = await ProcessNewSpDeal(dealModel, cancellationToken);
                        break;
                    }
                    default:
                    {
                        return Result.Failure<bool>(DataMigrationErrors.NotFound); 
                    }
                }
                break;
            }
            default:
            {
                return Result.Failure<bool>(DataMigrationErrors.NotFound);
            }
        }
        
        return true;
    }
}