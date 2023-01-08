namespace SampleApp.Application.Gateways;

using SampleApp.Core;
using SampleApp.Core.Entities;
using System.Linq.Expressions;

public interface IEfCommandRepository<TKey, TEntity> : IScoped
    where TEntity : IHasKey<TKey>
    where TKey : struct
{
    ValueTask<TEntity> GetAsync(TKey id);

    Task<List<TEntity>> GetAllByIdsAsync(IEnumerable<TKey> ids);

    Task<List<TEntity>> GetAllAsync();

    void Add(TEntity entity);

    Task AddAsync(TEntity entity);

    void AddRange(IEnumerable<TEntity> entities);

    Task AddRangeAsync(IEnumerable<TEntity> entities);

    //void AddSequential(TEntity entity);

    //Task AddSequentialAsync(TEntity entity);

    //void AddRangeSequential(List<TEntity> entities);

    //Task AddRangeSequentialAsync(List<TEntity> entities);

    void BulkInsert(List<TEntity> entities);

    Task BulkInsertAsync(List<TEntity> entities);

    Task<TEntity> GetOrCreateAsync(Expression<Func<TEntity, bool>> predicate, Func<TEntity> createFunc);

    //void Update(TEntity entity);

    //void BulkUpdate(List<TEntity> entities);

    //Task BulkUpdateAsync(List<TEntity> entities);

    void Delete(TEntity entity);

    Task DeleteAsync(TKey id);

    void DeleteRange(IEnumerable<TEntity> entities);

    Task DeleteRangeAsync(IEnumerable<TKey> ids);

    void BulkDelete(List<TEntity> entities);

    Task BulkDeleteAsync(List<TEntity> entities);

    bool Exist(TKey id);

    Task<bool> ExistAsync(TKey id);

    int Count(IEnumerable<TKey> ids);

    Task<int> CountAsync(IEnumerable<TKey> ids);
}