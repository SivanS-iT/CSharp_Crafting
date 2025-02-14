using Domain.Abstractions;
using Domain.Primitives;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Generic;

internal abstract class GenericRepository<TEntity>(AppDbContext dbContext) 
    : IGenericRepository<TEntity> where TEntity : Entity
{
    protected readonly AppDbContext DbContext = dbContext;

    
    public virtual async Task<TEntity?> GetById(int id, CancellationToken cancellationToken )
    {
        return await DbContext.Set<TEntity>().FindAsync(id);
    }
    
    public async Task<List<TEntity>> GetAll(CancellationToken cancellationToken )
    {
        return await DbContext.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public void Add(TEntity entity, CancellationToken cancellationToken)
    {
        DbContext.Set<TEntity>().Add(entity);
    }

    public void Update(TEntity entity, CancellationToken cancellationToken )
    {
        DbContext.Set<TEntity>().Update(entity);
    }

    public void Delete(TEntity entity, CancellationToken cancellationToken )
    {
        DbContext.Set<TEntity>().Remove(entity);
    }
}