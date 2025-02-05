using MediatR;

namespace Application.Features.Auth.Commands.Logout;

public class LogoutUserCommand : IRequest<bool>
{
    public Guid UserId { get; set; }
}