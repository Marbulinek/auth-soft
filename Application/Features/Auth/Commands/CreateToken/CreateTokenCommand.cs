using Domain.Entities;
using MediatR;

namespace Application.Features.Auth.Commands.CreateToken;

public class CreateTokenCommand : IRequest<string>
{
    public User User { get; set; }
}