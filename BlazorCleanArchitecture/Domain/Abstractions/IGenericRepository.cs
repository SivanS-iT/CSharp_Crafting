using Domain.Primitives;

namespace Domain.Abstractions;

public interface IGenericRepository<TEntity> where TEntity : Entity
{
    Task<TEntity?> GetById(int id, CancellationToken cancellationToken );

    Task<List<TEntity>> GetAll(CancellationToken cancellationToken );
    
    void Add(TEntity entity, CancellationToken cancellationToken);
    
    void Update(TEntity entity, CancellationToken cancellationToken );
    
    void Delete(TEntity entity, CancellationToken cancellationToken );
}