using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Contracts;
using UzEx.Analytics.Domain.Contracts.Errors;

namespace UzEx.Analytics.Application.Contracts.GetContract;

public sealed class GetContractQueryHandler: IQueryHandler<GetContractQuery, GetContractResponse>
{
    private readonly IContractRepository _contractRepository;

    public GetContractQueryHandler(IContractRepository contractRepository)
    {
        _contractRepository = contractRepository;
    }

    public async Task<Result<GetContractResponse>> Handle(GetContractQuery request, CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.GetByIdAsync(request.Id, cancellationToken);

        if (contract is null)
        {
            return Result.Failure<GetContractResponse>(ContractErrors.NotFound);
        }

        var response = new GetContractResponse
        {
            Id = contract.Id,
            BasePrice = contract.BasePrice.Value,
            BusinessKey = contract.BusinessKey.Value,
            Form = contract.Form.ToString(),
            Lot = contract.Lot.Value,
            Number = contract.Number.Value,
            Type = contract.Type.ToString(),
            Unit = contract.Unit.Value
        };

        return response;
    }
}