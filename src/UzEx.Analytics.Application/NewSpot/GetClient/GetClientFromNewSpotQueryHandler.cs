using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Abstractions.NewSpot;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Clients.Errors;

namespace UzEx.Analytics.Application.NewSpot.GetClient;

public sealed class GetClientFromNewSpotQueryHandler : IQueryHandler<GetClientFromNewSpotQuery, GetClientFromNewSpotResponse>
{
    private readonly INewSpotService _newSpotService;

    public GetClientFromNewSpotQueryHandler(INewSpotService newSpotService)
    {
        _newSpotService = newSpotService;
    }

    public async Task<Result<GetClientFromNewSpotResponse>> Handle(GetClientFromNewSpotQuery request, CancellationToken cancellationToken)
    {
        var client = await _newSpotService.GetClient(request.id, cancellationToken);


        if (client is null)
        {
            return Result.Failure<GetClientFromNewSpotResponse>(ClientErrors.NotFound);
        }

        var response = new GetClientFromNewSpotResponse
        {
            Id = client.Id,
            AccountType = client.AccountType,
            Tin = client.Tin,
            Name = client.Name,
            CountryCode = client.CountryCode,
            RegionCode = client.RegionCode,
            DistrictCode = client.DistrictCode,
            Address = client.Address
        };

        return response;
    }
}
