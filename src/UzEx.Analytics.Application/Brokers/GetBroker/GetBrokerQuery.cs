using UzEx.Analytics.Application.Abstractions.Messaging;

namespace UzEx.Analytics.Application.Brokers.GetBroker;

public sealed record GetBrokerQuery(Guid Id) : IQuery<GetBrokerResponse>;