using UzEx.Analytics.Application.Abstractions.Messaging;

namespace UzEx.Analytics.Application.NewSpot.GetBroker;

public sealed record GetBrokerFromNewSpotQuery(string id) : IQuery<GetBrokerFromNewSpotResponse>;
