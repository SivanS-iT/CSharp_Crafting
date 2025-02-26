namespace Application.Abstractions.Data;

/// <summary>
/// Interface that groupes multiple database operations into a single transaction
/// </summary>
public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}