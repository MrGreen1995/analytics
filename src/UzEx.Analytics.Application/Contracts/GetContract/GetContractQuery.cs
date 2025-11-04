using UzEx.Analytics.Application.Abstractions.Messaging;

namespace UzEx.Analytics.Application.Contracts.GetContract;

public sealed record GetContractQuery(Guid Id) : IQuery<GetContractResponse>;