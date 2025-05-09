using System.Linq.Expressions;
using SimpleWallet.Domain.Entities.Base;

namespace SimpleWallet.Infraestructure.Repositories.Interfaces.Base;

/// <summary>
/// Defines a generic repository interface for performing CRUD operations on entities of type <typeparamref name="TEntity"/>.
/// </summary>
/// <typeparam name="TEntity">The type of the entity that the repository will manage. Must inherit from <see cref="BaseEntity"/>.</typeparam>
public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    /// Retrieves an entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found, or <c>null</c> if not.</returns>
    Task<TEntity?> GetByIdAsync(int id);

    /// <summary>
    /// Retrieves all entities of type <typeparamref name="TEntity"/>.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IQueryable{T}"/> of all entities.</returns>
    Task<IQueryable<TEntity>> GetAllAsync();

    /// <summary>
    /// Retrieves all entities of type <typeparamref name="TEntity"/> that match the specified condition.
    /// </summary>
    /// <param name="where">An expression that defines the condition to filter the entities.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IQueryable{T}"/> of matching entities.</returns>
    Task<IQueryable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> @where);

    /// <summary>
    /// Adds a new entity to the repository.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the added entity.</returns>
    Task<TEntity> AddAsync(TEntity entity);

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the updated entity.</returns>
    Task<TEntity> UpdateAsync(TEntity entity);

    /// <summary>
    /// Deletes an entity from the repository.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteAsync(TEntity entity);

    /// <summary>
    /// Saves all changes made in the repository to the underlying data store.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}