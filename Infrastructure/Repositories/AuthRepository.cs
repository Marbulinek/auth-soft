using Domain.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AuthRepository(AppDbContext dbContext) : IAuthRepository
{
    public async Task<bool> ExistsUserWithUsernameAsync(string username)
    {
        return await dbContext.Users.AnyAsync(user => user.Username == username);
    }

    public async Task AddUserAsync(User user)
    {
        await dbContext.Users.AddAsync(user);
    }

    public async Task<User?> FindUserByUsername(string username)
    {
        return await dbContext.Users.FirstOrDefaultAsync(x => x.Username == username);
    }

    public async Task<User?> FindByIdAsync(Guid id)
    {
        return await dbContext.Users.FindAsync(id);
    }
}