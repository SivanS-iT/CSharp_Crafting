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

    public async Task Add(TEntity entity, CancellationToken cancellationToken)
    {
        await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public void Update(TEntity entity)
    {
        DbContext.Set<TEntity>().Update(entity);
    }

    public void Delete(TEntity entity)
    {
        DbContext.Set<TEntity>().Remove(entity);
    }
}