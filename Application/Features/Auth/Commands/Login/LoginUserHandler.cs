using Domain.Abstractions;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Commands.Login;

public class LoginUserHandler(
    IAuthRepository authRepository,
    LoginUserCommandValidator commandValidator
) : IRequestHandler<LoginUserCommand, User?>
{
    public async Task<User?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        await commandValidator.ValidateAndThrowAsync(request, cancellationToken: cancellationToken);

        var user = await authRepository.FindUserByUsername(request.Username);
        if (user is null)
        {
            return null;
        }

        if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) ==
            PasswordVerificationResult.Failed)
        {
            return null;
        }

        return user;
    }
}