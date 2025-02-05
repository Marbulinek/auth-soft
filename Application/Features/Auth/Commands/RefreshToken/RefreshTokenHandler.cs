using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenHandler(IAuthRepository authRepository) : IRequestHandler<RefreshTokenCommand, User?>
{
    public async Task<User?> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await ValidateRefreshTokenAsync(request.RefreshTokenRequestDto.UserId, request.RefreshTokenRequestDto.RefreshToken);
        if (user is null)
        {
            return null;
        }

        return user;
    }
    
    private async Task<User?> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
    {
        var user = await authRepository.FindByIdAsync(userId);
        if(user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime < DateTime.UtcNow)
        {
            return null;
        }

        return user;
    }
}