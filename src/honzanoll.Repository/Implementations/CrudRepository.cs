using Microsoft.EntityFrameworkCore;
using honzanoll.Data.Models;
using honzanoll.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace honzanoll.Repository.Implementations
{
    /// <summary>
    /// Common repository implements basic CRUD operations
    /// </summary>
    /// <typeparam name="TEntity">The entity</typeparam>
    public class CrudRepository<TEntity, TSqlContext> : IRepository<TEntity>
        where TEntity : ModelBase
        where TSqlContext : DbContext
    {
        #region Fields

        /// <summary>
        /// Default database context
        /// </summary>
        protected readonly TSqlContext sqlContext;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="sqlContext">Injected default database context</param>
        public CrudRepository(TSqlContext sqlContext)
        {
            this.sqlContext = sqlContext;
        }

        #endregion

        #region Public methods

        #region Create

        /// <summary>
        /// Add new entity to database
        /// </summary>
        /// <param name="entity">The new entity</param>
        /// <returns></returns>
        public async Task CreateAsync(TEntity entity)
        {
            sqlContext.Set<TEntity>().Add(entity);

            await sqlContext.SaveChangesAsync();
        }

        /// <summary>
        /// Add new entities to database
        /// </summary>
        /// <param name="entities">The new entities</param>
        /// <returns></returns>
        public async Task CreateAsync(IEnumerable<TEntity> entities)
        {
            sqlContext.Set<TEntity>().AddRange(entities);

            await sqlContext.SaveChangesAsync();
        }

        #endregion

        #region Read

        /// <summary>
        /// Read all entities from database
        /// </summary>
        /// <returns>All entities</returns>
        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await sqlContext.Set<TEntity>()
                .AsNoTracking()
                .OrderBy(e => e.Title)
                .ToListAsync();
        }

        /// <summary>
        /// Read all entities from database
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>All entities match predicate</returns>
        public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await sqlContext.Set<TEntity>()
                .AsNoTracking()
                .Where(predicate)
                .OrderBy(e => e.Title)
                .ToListAsync();
        }

        /// <summary>
        /// Get one entity from database
        /// </summary>
        /// <param name="id">The entity id</param>
        /// <returns>The entity</returns>
        public virtual async Task<TEntity> GetAsync(Guid id)
        {
            return await sqlContext.Set<TEntity>()
                .SingleOrDefaultAsync(d => id.Equals(d.Id));
        }

        /// <summary>
        /// Get one entity from database
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>The entity</returns>
        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await sqlContext.Set<TEntity>()
                .AsNoTracking()
                .SingleOrDefaultAsync(predicate);
        }

        #endregion

        #region Update

        /// <summary>
        /// Update entity in database
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns></returns>
        public async Task UpdateAsync(TEntity entity)
        {
            sqlContext.Set<TEntity>().Update(entity);

            await sqlContext.SaveChangesAsync();
        }

        /// <summary>
        /// Update entities in database
        /// </summary>
        /// <param name="entity">The entities to update</param>
        /// <returns></returns>
        public async Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                sqlContext.Set<TEntity>().Update(entity);
            }

            await sqlContext.SaveChangesAsync();
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete entity from database
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(TEntity entity)
        {
            sqlContext.Set<TEntity>().Remove(entity);

            await sqlContext.SaveChangesAsync();
        }

        #endregion

        #endregion
    }
}
