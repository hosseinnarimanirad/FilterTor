namespace SampleApp.Application.Gateways;

using SampleApp.Core;
using SampleApp.Core.Entities;
using System.Linq.Expressions;

public interface IEfQueryRepository<TKey, TEntity> : IScoped
    where TEntity : IHasKey<TKey>
    where TKey : struct
{
    Task<TEntity> GetAsync(TKey key);

    Task<List<TEntity>> GetAllAsync();

    Task<List<TEntity>> GetAllByIdsAsync(List<TKey> keys);

}