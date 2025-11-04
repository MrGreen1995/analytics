using MediatR;
using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> where TQuery : IQuery<TResponse>
{
}