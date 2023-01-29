namespace SampleApp.Application.Gateways;

using SampleApp.Core;
using SampleApp.Core.Entities;
using System.Linq.Expressions;

public interface ICommandRepository<TKey, TEntity> : IScoped
    where TEntity : IHasKey<TKey>
    where TKey : struct
{
    #region Get

    ValueTask<TEntity> GetAsync(TKey id);

    Task<List<TEntity>> GetAllByIdsAsync(IEnumerable<TKey> ids);

    Task<List<TEntity>> GetAllAsync();

    #endregion


    #region Add

    void Add(TEntity entity);

    Task AddAsync(TEntity entity);

    void AddRange(IEnumerable<TEntity> entities);

    Task AddRangeAsync(IEnumerable<TEntity> entities);

    void BulkAdd(List<TEntity> entities);

    Task BulkAddAsync(List<TEntity> entities);

    #endregion

     
    #region Remove

    void Remove(TEntity entity);

    Task RemoveAsync(TKey id);

    void RemoveRange(IEnumerable<TEntity> entities);

    Task RemoveRangeAsync(IEnumerable<TKey> ids);

    void BulkRemove(List<TEntity> entities);

    Task BulkRemoveAsync(List<TEntity> entities);

    #endregion


    #region Exists

    bool Exists(TKey id);

    Task<bool> ExistAsync(TKey id);

    #endregion


    #region Count

    int Count(IEnumerable<TKey> ids);

    Task<int> CountAsync(IEnumerable<TKey> ids);

    #endregion

    Task<TEntity> GetOrCreateAsync(Expression<Func<TEntity, bool>> predicate, Func<TEntity> createFunc);
}