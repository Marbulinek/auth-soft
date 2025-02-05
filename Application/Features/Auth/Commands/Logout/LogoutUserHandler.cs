using Domain.Abstractions;
using MediatR;

namespace Application.Features.Auth.Commands.Logout;

public class LogoutUserHandler(IAuthRepository authRepository, IUnitOfWork unitOfWork) : IRequestHandler<LogoutUserCommand, bool>
{
    public async Task<bool> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        var user = await authRepository.FindByIdAsync(request.UserId);
        if (user is null)
        {
            return false;
        }
        
        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = null;
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}