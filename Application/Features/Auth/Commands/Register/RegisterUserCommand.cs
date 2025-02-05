using Domain.Entities;
using MediatR;

namespace Application.Features.Auth.Commands.Register;

public class RegisterUserCommand : IRequest<User?>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}