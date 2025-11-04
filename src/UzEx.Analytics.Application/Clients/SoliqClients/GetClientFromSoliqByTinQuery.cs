using UzEx.Analytics.Application.Abstractions.Messaging;

namespace UzEx.Analytics.Application.Clients.SoliqClients;

public record GetClientFromSoliqByTinQuery(string Tin) : IQuery<GetClientFromSoliqByTinResponse>;
