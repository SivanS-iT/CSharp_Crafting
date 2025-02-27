using Domain.Primitives;

namespace Domain.Abstractions;


/// <summary>
/// Defining a generic operation for all repostories
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IGenericRepository<TEntity> where TEntity : Entity
{
    Task<TEntity?> GetById(int id, CancellationToken cancellationToken );

    Task<List<TEntity>?> GetAll(CancellationToken cancellationToken);
    
    Task Add(TEntity entity, CancellationToken cancellationToken);
    
    void Update(TEntity entity);
    
    void Delete(TEntity entity);
}