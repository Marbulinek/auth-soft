using FluentValidation;

namespace Application.Features.Auth.Commands.Register;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(command => command.Username)
            .NotEmpty()
            .WithMessage("Email should not be empty!")
            .EmailAddress()
            .WithMessage("Email should be valid!");

        RuleFor(command => command.Password)
            .NotEmpty()
            .WithMessage("Password should not be empty!")
            .MinimumLength(6)
            .WithMessage("Password should have at least 6 characters!");
    }
}