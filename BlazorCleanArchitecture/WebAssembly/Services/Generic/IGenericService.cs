using WebAssembly.Data;

namespace WebAssembly.Services.Generic;

public interface IGenericService<TEntity> where TEntity : Entity
{
    Task<TEntity?> GetById(int id, CancellationToken cancellationToken );

    Task<List<TEntity>?> GetAll(CancellationToken cancellationToken);
    
    Task<int> Add(TEntity entity, CancellationToken cancellationToken);
    
    void Update(TEntity entity);
    
    void Delete(int i);
}