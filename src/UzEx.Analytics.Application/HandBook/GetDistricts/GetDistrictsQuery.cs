using UzEx.Analytics.Application.Abstractions.Messaging;

namespace UzEx.Analytics.Application.HandBook.GetDistricts;

public record GetDistrictsQuery(int regionId) : IQuery<List<GetDistrictsResponse>>;
