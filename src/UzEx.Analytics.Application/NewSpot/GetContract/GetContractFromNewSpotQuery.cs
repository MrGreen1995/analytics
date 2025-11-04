using UzEx.Analytics.Application.Abstractions.Messaging;

namespace UzEx.Analytics.Application.NewSpot.GetContract;

public sealed record GetContractFromNewSpotQuery(long id) : IQuery<GetContractFromNewSpotResponse>;