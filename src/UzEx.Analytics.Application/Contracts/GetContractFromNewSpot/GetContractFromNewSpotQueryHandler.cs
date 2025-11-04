using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Abstractions.NewSpot;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Contracts.Errors;

namespace UzEx.Analytics.Application.Contracts.GetContractFromNewSpot;

public sealed class GetContractFromNewSpotQueryHandler : IQueryHandler<GetContractFromNewSpotQuery, GetContractFromNewSpotResponse>
{
    private readonly INewSpotService _newSpotService ;

    public GetContractFromNewSpotQueryHandler(INewSpotService newSpotService)
    {
        _newSpotService = newSpotService;
    }

    public async Task<Result<GetContractFromNewSpotResponse>> Handle(GetContractFromNewSpotQuery request, CancellationToken cancellationToken)
    {
        var contract = await _newSpotService.GetContract(request.Id, cancellationToken);

        if (contract == null)
        {
            return Result.Failure<GetContractFromNewSpotResponse>(ContractErrors.NotFound); 
        }

        var response = new GetContractFromNewSpotResponse
        {
            Id = contract.Id,
            ProductId = contract.ProductId,
            Number = contract.Number,
            Name = contract.Name,
            Currency = contract.Currency,
            BasePrice = contract.BasePrice,
            Form = contract.Form,
            Type = contract.Type,
            Lot = contract.Lot,
            TradeType = contract.TradeType,
            Unit = contract.Unit,
            Warehouse = contract.Warehouse,
            DeliveryBase = contract.DeliveryBase,
            OriginCOuntry = contract.OriginCountryId
        };
        
        return response;
    }
}