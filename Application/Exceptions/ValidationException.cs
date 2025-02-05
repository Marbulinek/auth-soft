using Domain.Entities;

namespace Application.Exceptions;

public class ValidationException : Exception
{
    public List<ValidationError> Errors { get; }

    public ValidationException(List<ValidationError> errors)
        : base("Validation failed")
    {
        Errors = errors;
    }
}