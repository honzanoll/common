using honzanoll.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace honzanoll.Repository.Abstractions
{
    /// <summary>
    /// Common repository implements basic CRUD operations
    /// </summary>
    /// <typeparam name="TEntity">The entity</typeparam>
    public interface IRepository<TEntity>
        where TEntity : IEntity
    {
        #region Public methods

        #region Create

        /// <summary>
        /// Add new entity to database
        /// </summary>
        /// <param name="entity">The new entity</param>
        /// <returns></returns>
        Task CreateAsync(TEntity entity);

        /// <summary>
        /// Add new entities to database
        /// </summary>
        /// <param name="entities">The new entities</param>
        /// <returns></returns>
        Task CreateAsync(IEnumerable<TEntity> entities);

        #endregion

        #region Read

        /// <summary>
        /// Read all entities from database
        /// </summary>
        /// <returns>All entities</returns>
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// Read all entities from database
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>All entities match predicate</returns>
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Get one entity from database
        /// </summary>
        /// <param name="id">The entity id</param>
        /// <returns>The entity</returns>
        Task<TEntity> GetAsync(Guid id);

        /// <summary>
        /// Get one entity from database
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>The entity</returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);

        #endregion

        #region Update

        /// <summary>
        /// Update entity in database
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Update entities in database
        /// </summary>
        /// <param name="entity">The entities to update</param>
        /// <returns></returns>
        Task UpdateAsync(IEnumerable<TEntity> entities);

        #endregion

        #region Delete

        /// <summary>
        /// Delete entity from database
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns></returns>
        Task DeleteAsync(TEntity entity);

        #endregion

        #endregion
    }
}
