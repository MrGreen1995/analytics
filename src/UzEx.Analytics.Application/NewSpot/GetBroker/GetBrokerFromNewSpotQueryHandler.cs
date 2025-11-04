using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Abstractions.NewSpot;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Brokers.Errors;

namespace UzEx.Analytics.Application.NewSpot.GetBroker;

public sealed class GetBrokerFromNewSpotQueryHandler : IQueryHandler<GetBrokerFromNewSpotQuery, GetBrokerFromNewSpotResponse>
{
    private readonly INewSpotService _newSpotService;

    public GetBrokerFromNewSpotQueryHandler(INewSpotService newSpotService)
    {
        _newSpotService = newSpotService;
    }

    public async Task<Result<GetBrokerFromNewSpotResponse>> Handle(GetBrokerFromNewSpotQuery request, CancellationToken cancellationToken)
    {
        var broker = await _newSpotService.GetBroker(request.id, cancellationToken);

        if (broker is null)
        {
            return Result.Failure<GetBrokerFromNewSpotResponse>(BrokerErrors.NotFound);
        }

        var response = new GetBrokerFromNewSpotResponse
        {
            Id = broker.Id,
            AccountType = broker.AccountType,
            Tin = broker.Tin,
            BrokerNumber = broker.BrokerNumber,
            Name = broker.Name,
            CountryCode = broker.CountryCode,
            RegionCode = broker.RegionCode,
            DistrictCode = broker.DistrictCode,
            Address = broker.Address
        };

        return response;
    }
}
