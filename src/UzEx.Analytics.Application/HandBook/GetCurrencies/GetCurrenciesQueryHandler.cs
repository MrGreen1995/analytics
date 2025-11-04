using UzEx.Analytics.Application.Abstractions.HandBook;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Application.HandBook.GetCurrencies;

public class GetCurrenciesQueryHandler : IQueryHandler<GetCurrenciesQuery, List<GetCurrenciesResponse>>
{
    private readonly IHandBookService _handBookService;

    public GetCurrenciesQueryHandler(IHandBookService handBookService)
    {
        _handBookService = handBookService;
    }

    public async Task<Result<List<GetCurrenciesResponse>>> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
    {
        var currencies = await _handBookService.GetAllCurrencyAsync(cancellationToken);

        var response = currencies
            .Select(currency => new GetCurrenciesResponse
            {
                Id = currency.Id,
                Name = currency.Name,
                Code = currency.Code,
                Number = currency.Number,
                ShortName = currency.ShortName,
            })
            .ToList();

        return response;
    }
}
