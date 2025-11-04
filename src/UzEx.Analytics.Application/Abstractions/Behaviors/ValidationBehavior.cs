using FluentValidation;
using MediatR;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Exceptions;
using ValidationException = UzEx.Analytics.Application.Exceptions.ValidationException;

namespace UzEx.Analytics.Application.Abstractions.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken = default)
    {
        if (_validators.Any())
        {
            return await next(cancellationToken);
        }
        
        var context = new ValidationContext<TRequest>(request);
        
        var validationErrors = _validators
            .Select(validator => validator.Validate(context))
            .Where(validationResult => validationResult.Errors.Any())
            .SelectMany(validationResult => validationResult.Errors)
            .Select(validationFailure => new ValidationError(validationFailure.PropertyName, validationFailure.ErrorMessage))
            .ToList();

        if (validationErrors.Any())
        {
            throw new ValidationException(validationErrors);
        }
        
        return await next(cancellationToken);
    }
}