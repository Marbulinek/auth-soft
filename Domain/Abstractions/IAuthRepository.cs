using Domain.Entities;

namespace Domain.Abstractions;

public interface IAuthRepository
{
    Task<bool> ExistsUserWithUsernameAsync(string username);

    Task AddUserAsync(User user);

    Task<User?> FindUserByUsername(string username);

    Task<User?> FindByIdAsync(Guid id);
}