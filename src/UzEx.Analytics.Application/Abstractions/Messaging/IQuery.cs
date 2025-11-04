using MediatR;
using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
