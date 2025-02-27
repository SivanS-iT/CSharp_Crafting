using Application.Abstractions.Data;

namespace Infrastructure.Data;

/// <summary>
/// Represents Unit of Work pattern to manage database transactions.
/// </summary>
/// <param name="appDbContext"></param>
public class UnitOfWork (AppDbContext appDbContext) : IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return appDbContext.SaveChangesAsync(cancellationToken);
    }
}