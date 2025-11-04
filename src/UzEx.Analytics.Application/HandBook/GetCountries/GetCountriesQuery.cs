using UzEx.Analytics.Application.Abstractions.Messaging;

namespace UzEx.Analytics.Application.HandBook.GetCountries;

public sealed record GetCountriesQuery() : IQuery<List<GetCountriesResponse>>;
