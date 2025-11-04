using UzEx.Analytics.Application.Abstractions.Messaging;

namespace UzEx.Analytics.Application.Contracts.GetContractFromNewSpot;

public sealed record GetContractFromNewSpotQuery(long Id) : IQuery<GetContractFromNewSpotResponse>;
