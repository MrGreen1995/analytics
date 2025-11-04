using System.Net.Http.Json;
using UzEx.Analytics.Application.Abstractions.NewSpot;
using UzEx.Analytics.Application.Models.NewSpot;
using UzEx.Analytics.Infrastructure.Dtos.NewSpot;

namespace UzEx.Analytics.Infrastructure.NewSpot;

public sealed class NewSpotService : INewSpotService
{
    private readonly HttpClient _httpClient;

    public NewSpotService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyList<NewSpotOrderModel>> GetOrders(DateTime date, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetFromJsonAsync<NewSpotResponse<IReadOnlyList<OrderDto>>>(
            $"/api/Report/GetOrdersForBi?date={date:ddMMyyyy}",
            cancellationToken);

        if (response is not { Success: true })
        {
            return new List<NewSpotOrderModel>();
        }

        var dto = response.Data;

        if (dto == null)
        {
            return new List<NewSpotOrderModel>();
        }
        
        var orders = dto.Select(a => new NewSpotOrderModel
        {
            ClientId = a.ClientId,
            BrokerId = a.BrokerId,
            Id = a.Id,
            Direction = a.Direction,
            Price = a.Price,
            Amount = a.Amount,
            ContractId = a.ContractId,
            Date = a.Date,
            Status = a.Status,
            ParentId = a.ParentId
        }).ToList();
        
        return orders;
    }

    public async Task<IReadOnlyList<NewSpotDealModel>> GetDeals(DateTime date, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetFromJsonAsync<NewSpotResponse<IReadOnlyList<NewSpotDealDto>>>(
            $"/api/Report/GetDealsForBi?date={date:ddMMyyyy}",
            cancellationToken);

        if (response is not { Success: true })
        {
            return new List<NewSpotDealModel>();
        }

        var dto = response.Data;

        if (dto == null)
        {
            return new List<NewSpotDealModel>();
        }

        return dto.Select(deal => new NewSpotDealModel
        {
            Id = deal.Id,
            ModifiedDate = deal.ModifiedDate,
            Number = deal.Number,
            DealDate = deal.DealDate,
            ContractId = deal.ContractId,
            SellerClientId = deal.SellerClientId,
            SellerBrokerId = deal.SellerBrokerId,
            BuyerClientId = deal.BuyerClientId,
            BuyerBrokerId = deal.BuyerBrokerId,
            Amount = deal.Amount,
            Price = deal.Price,
            PriceCurrency = deal.PriceCurrency,
            Cost = deal.Cost,
            CostCurrency = deal.CostCurrency,
            Status = deal.Status,
            PaymentDays = deal.PaymentDays,
            DeliveryDays = deal.DeliveryDays,
            PaymentDate = deal.PaymentDate,
            CloseDate = deal.CloseDate,
            SellerTradeCommissionSum = deal.SellerTradeCommissionSum,
            SellerTradeCommissionCurrency = deal.SellerTradeCommissionCurrency,
            SellerClearingCommissionSum = deal.SellerClearingCommissionSum,
            SellerClearingCommissionCurrency = deal.SellerClearingCommissionCurrency,
            SellerPledgeSum = deal.SellerPledgeSum,
            SellerPledgeCurrency = deal.SellerPledgeCurrency,
            BuyerTradeCommissionSum = deal.BuyerTradeCommissionSum,
            BuyerTradeCommissionCurrency = deal.BuyerTradeCommissionCurrency,
            BuyerClearingCommissionSum = deal.BuyerClearingCommissionSum,
            BuyerClearingCommissionCurrency = deal.BuyerClearingCommissionCurrency,
            BuyerPledgeSum = deal.BuyerPledgeSum,
            BuyerPledgeCurrency = deal.BuyerPledgeCurrency
        }).ToList();
    }

    public async Task<NewSpotContractModel?> GetContract(long id, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetFromJsonAsync<NewSpotResponse<NewSpotContractDto>>(
            $"/api/Report/GetContractForBi?id={id}",
            cancellationToken);
        
        if (response is not { Success: true })
        {
            return null;
        }

        var dto = response.Data;

        if (dto == null)
        {
            return null;
        }
        
        var contract = new NewSpotContractModel
        {
            Id = dto.Id,
            ProductId = dto.ProductId,
            Number = dto.Number,
            Name = dto.Name,
            TradeType = dto.TradeType,
            Type = dto.Type,
            Form = dto.Form,
            Lot = dto.Lot,
            Unit = dto.Unit,
            BasePrice = dto.BasePrice,
            Currency = dto.Currency,
            DeliveryBase = dto.DeliveryBase,
            Warehouse = dto.Warehouse,
            OriginCountryId = dto.OriginCountry
        };
        
        return contract;
    }

    public async Task<NewSpotClientModel?> GetClient(string id, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetFromJsonAsync<NewSpotResponse<NewSpotClientDto>>(
            $"/api/Report/GetClientForBi?id={id}",
            cancellationToken);
        
        if (response is not { Success: true })
        {
            return null;
        }

        var dto = response.Data;

        if (dto == null)
        {
            return null;
        }
        
        var client = new NewSpotClientModel
        {
            Id =  dto.Id,
            AccountType = dto.AccountType,
            Tin = dto.Tin,
            Name = dto.Name,
            CountryCode = dto.CountryCode,
            RegionCode = dto.RegionCode,
            DistrictCode = dto.DistrictCode,
            Address = dto.Address
        };
        
        return client;
    }

    public async Task<NewSpotBrokerModel?> GetBroker(string id, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetFromJsonAsync<NewSpotResponse<NewSpotBrokerDto>>(
            $"/api/Report/GetBrokerForBi?id={id}",
            cancellationToken);
        
        if (response is not { Success: true })
        {
            return null;
        }

        var dto = response.Data;

        if (dto == null)
        {
            return null;
        }
        
        var broker = new NewSpotBrokerModel()
        {
            Id =  dto.Id,
            AccountType = dto.AccountType,
            Tin = dto.Tin,
            BrokerNumber = dto.BrokerNumber,
            Name = dto.Name,
            CountryCode = dto.CountryCode,
            RegionCode = dto.RegionCode,
            DistrictCode = dto.DistrictCode,
            Address = dto.Address
        };
        
        return broker;
    }
}