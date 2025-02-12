using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors;

public class ValidationPipelineBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators,
    ILogger<ValidationPipelineBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        
        var validationFailures = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        
        var errors = validationFailures.Where(validationResult => !validationResult.IsValid)
                                                         .SelectMany(validationResult => validationResult.Errors)
                                                         .Select(validationFailure => new ValidationError(
                                                             validationFailure.PropertyName,
                                                             validationFailure.ErrorMessage))
                                                         .ToList();

        if (errors.Count != 0)
        {
            logger.LogError("Validation errors - {Errors}", string.Join(", ", errors.Select(e => e.errorMessage)));
            throw new Exceptions.ValidationException(errors);
        }

        var response = await next();

        return response;
    }
}