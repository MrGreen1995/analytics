using MediatR;
using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{
}

public interface IBaseCommand
{
}