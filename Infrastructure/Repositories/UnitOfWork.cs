using Domain.Abstractions;

namespace Infrastructure.Repositories;

/**
 * The UnitOfWork class is responsible for saving changes to the database.
 */
public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}