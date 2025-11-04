using UzEx.Analytics.Application.Abstractions.Messaging;

namespace UzEx.Analytics.Application.NewSpot.GetClient;

public sealed record GetClientFromNewSpotQuery(string id) : IQuery<GetClientFromNewSpotResponse>;
