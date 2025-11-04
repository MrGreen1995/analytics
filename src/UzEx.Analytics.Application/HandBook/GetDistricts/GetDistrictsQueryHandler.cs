using UzEx.Analytics.Application.Abstractions.HandBook;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Application.HandBook.GetDistricts;

public class GetDistrictsQueryHandler : IQueryHandler<GetDistrictsQuery, List<GetDistrictsResponse>>
{
    private readonly IHandBookService _handBookService;

    public GetDistrictsQueryHandler(IHandBookService handBookService)
    {
        _handBookService = handBookService;
    }

    public async Task<Result<List<GetDistrictsResponse>>> Handle(GetDistrictsQuery request, CancellationToken cancellationToken)
    {
        var districts = await _handBookService.GetAllDistrictsFromUzbRegion(request.regionId, cancellationToken);

        var response = districts
           .Select(district => new GetDistrictsResponse
           {
               Id = district.Id,
               Name = district.Name,
               Code = district.Code,
               Number = district.Number,
               ShortName = district.ShortName,
               RegionId = district.RegionId,
               RegionName = district.RegionName,
               RegionShortName = district.RegionShortName
           })
           .ToList();

        return response;
    }
}
