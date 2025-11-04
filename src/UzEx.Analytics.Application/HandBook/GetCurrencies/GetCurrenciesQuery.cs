using UzEx.Analytics.Application.Abstractions.Messaging;

namespace UzEx.Analytics.Application.HandBook.GetCurrencies;

public record GetCurrenciesQuery : IQuery<List<GetCurrenciesResponse>>;