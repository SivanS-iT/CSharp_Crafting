using Domain.Abstractions;
using Domain.Primitives;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Generic
{
    public abstract class GenericRepository<TEntity>(AppDbContext dbContext) : IGenericRepository<TEntity>
        where TEntity : Entity
    {
        /// <summary>
        /// Represents a session with the database.
        /// </summary>
        protected readonly AppDbContext DbContext = dbContext;

        public virtual async Task<TEntity?> GetById(int id)
        {
            return await DbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<IReadOnlyList<TEntity>> GetAll()
        {
            return await DbContext.Set<TEntity>().ToListAsync();
        }

        public void Add(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);
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
}
