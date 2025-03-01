using Domain.Abstractions;
using Domain.Primitives;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Generic;


/// <summary>
/// Provides a generic repository implementation for performing CRUD operations on entities.
/// </summary>
/// <param name="dbContext"></param>
/// <typeparam name="TEntity"></typeparam>
internal abstract class GenericRepository<TEntity>(AppDbContext dbContext) 
    : IGenericRepository<TEntity> where TEntity : Entity
{
    private readonly AppDbContext _dbContext = dbContext;

    
    public async Task<TEntity?> GetById(int id, CancellationToken cancellationToken )
    {
        return await _dbContext.Set<TEntity>().FindAsync(id, cancellationToken);
    }
    
    public async Task<List<TEntity>?> GetAll(CancellationToken cancellationToken )
    {
        return await _dbContext.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public async Task Add(TEntity entity, CancellationToken cancellationToken)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public void Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
    }
}