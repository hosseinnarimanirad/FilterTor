namespace SampleApp.Application.Gateways;

using FilterTor.Conditions;
using FilterTor.Decorators;
using FilterTor.Models;
using SampleApp.Core;
using SampleApp.Core.Entities;
using System.Linq.Expressions;

public interface IQueryRepository<TKey, TEntity> : IScoped
    where TEntity : IHasKey<TKey>
    where TKey : struct
{
    ValueTask<TEntity> GetAsync(TKey id);

    Task<List<TEntity>> GetAllByIdsAsync(IEnumerable<TKey> ids);

    Task<List<TEntity>> GetAllAsync();  
}
