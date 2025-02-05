using System.Security.Cryptography;
using Domain.Abstractions;
using MediatR;

namespace Application.Features.Auth.Commands.GenerateAndSaveRefreshToken;

public class GenerateAndSaveRefreshTokenHandler(IUnitOfWork unitOfWork) : IRequestHandler<GenerateAndSaveRefreshTokenCommand, string>
{
    public async Task<string> Handle(GenerateAndSaveRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = GenerateRefreshToken();
        request.User.RefreshToken = refreshToken;
        request.User.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(60);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return refreshToken;
    }
    
    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}