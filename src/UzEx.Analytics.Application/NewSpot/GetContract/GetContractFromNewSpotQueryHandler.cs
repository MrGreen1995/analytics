using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Abstractions.NewSpot;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Contracts.Errors;

namespace UzEx.Analytics.Application.NewSpot.GetContract;

public sealed class GetContractFromNewSpotQueryHandler : IQueryHandler<GetContractFromNewSpotQuery, GetContractFromNewSpotResponse>
{
    private readonly INewSpotService _newSpotService;

    public GetContractFromNewSpotQueryHandler(INewSpotService newSpotService)
    {
        _newSpotService = newSpotService;
    }

    public async Task<Result<GetContractFromNewSpotResponse>> Handle(GetContractFromNewSpotQuery request, CancellationToken cancellationToken)
    {
        var contract = await _newSpotService.GetContract(request.id, cancellationToken);

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
            TradeType = contract.TradeType,
            Type = contract.Type,
            Form = contract.Form,
            Lot = contract.Lot,
            Unit = contract.Unit,
            BasePrice = contract.BasePrice,
            Currency = contract.Currency,
            DeliveryBase = contract.DeliveryBase,
            Warehouse = contract.Warehouse,
            OriginCountryId = contract.OriginCountryId
        };

        return response;
    }
}
