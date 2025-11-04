using UzEx.Analytics.Application.Abstractions.HandBook;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Application.HandBook.GetRegions;

public class GetRegionsQueryHandler : IQueryHandler<GetRegionsQuery, List<GetRegionsResponse>>
{
    private readonly IHandBookService _handBookService;

    public GetRegionsQueryHandler(IHandBookService handBookService)
    {
        _handBookService = handBookService;
    }

    public async Task<Result<List<GetRegionsResponse>>> Handle(GetRegionsQuery request, CancellationToken cancellationToken)
    {
        var regions = await _handBookService.GetAllUzbRegionsAsync(cancellationToken);

        var response = regions.Select(region => new GetRegionsResponse
        {
            Id = region.Id,
            Name = region.Name,
            Code = region.Code,
            Number = region.Number,
            ShortName = region.ShortName,
            CountryId = region.CountryId,
            CountryName = region.CountryName,
            CountryShortName = region.CountryShortName
        }).ToList();

        return response;
    }
}
