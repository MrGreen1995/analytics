using MediatR;
using UzEx.Analytics.Application.Abstractions.HandBook;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Application.HandBook.GetCountries;

public sealed class GetCountriesQueryHandler : IQueryHandler<GetCountriesQuery, List<GetCountriesResponse>>
{
    private readonly IHandBookService _handBookService;

    public GetCountriesQueryHandler(IHandBookService handBookService)
    {
        _handBookService = handBookService;
    }

    async Task<Result<List<GetCountriesResponse>>> IRequestHandler<GetCountriesQuery, Result<List<GetCountriesResponse>>>.Handle(GetCountriesQuery request, CancellationToken cancellationToken)
    {
        var countries = await _handBookService.GetAllCountriesAsync(cancellationToken);
        
        var response = countries
            .Select(country => new GetCountriesResponse
            {
                Id = country.Id,
                Name = country.Name,
                ShortName = country.ShortName,
                Code = country.Code,
                Number = country.Number,
                OftenUse = country.OftenUse
            }).ToList();

        return response;
    }
}
