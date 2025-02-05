using FluentValidation;

namespace Application.Features.Auth.Commands.Login;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(command => command.Username)
            .NotEmpty()
            .WithMessage("Email nesmie byť prázdny!");

        RuleFor(command => command.Password)
            .NotEmpty()
            .WithMessage("Heslo nesmie byť prázdne!");
    }
}