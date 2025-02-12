using Application.Abstractions.Data;

namespace Infrastructure.Data;

public class UnitOfWork (AppDbContext appDbContext) : IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return appDbContext.SaveChangesAsync(cancellationToken);
    }
}