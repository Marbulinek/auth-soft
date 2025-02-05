namespace Domain.Abstractions;

/**
 * The IUnitOfWork interface is responsible for saving changes to the database.
 */
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}