using Domain.Entities;
using MediatR;

namespace Application.Features.Auth.Commands.GenerateAndSaveRefreshToken;

public class GenerateAndSaveRefreshTokenCommand : IRequest<string>
{
    public User User { get; set; }
}