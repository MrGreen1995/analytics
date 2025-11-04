using UzEx.Analytics.Application.Abstractions.Messaging;

namespace UzEx.Analytics.Application.NewSpot.GetOrders;

public sealed record GetOrdersFromNewSpotQuery(DateTime date) : IQuery<IReadOnlyList<GetOrdersFromNewSpotResponse>>;