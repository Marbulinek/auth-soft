using Application.Dtos;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<User?>
{
    public RefreshTokenRequestDto RefreshTokenRequestDto { get; set; }
}