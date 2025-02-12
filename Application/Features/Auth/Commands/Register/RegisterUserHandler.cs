using Domain.Abstractions;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Commands.Register;

public class RegisterUserHandler(
    IUnitOfWork unitOfWork,
    IAuthRepository authRepository,
    RegisterUserCommandValidator commandValidator) : IRequestHandler<RegisterUserCommand, User?>
{
    public async Task<User?> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        await commandValidator.ValidateAndThrowAsync(request, cancellationToken: cancellationToken);

        if (await authRepository.ExistsUserWithUsernameAsync(request.Username))
        {
            return null;
        }

        var user = new User();
        var hashedPassword = new PasswordHasher<User>().HashPassword(user, request.Password);
        user.Username = request.Username;
        user.PasswordHash = hashedPassword;

        await authRepository.AddUserAsync(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return user;
    }
}