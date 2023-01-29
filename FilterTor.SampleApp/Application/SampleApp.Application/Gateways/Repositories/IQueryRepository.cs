namespace SampleApp.Application.Gateways;

using FilterTor.Conditions;
using FilterTor.Decorators;
using FilterTor.Models;
using SampleApp.Core;
using SampleApp.Core.Entities;
using System.Linq.Expressions;

public interface IQueryRepository<TKey, TEntity> : IScoped, IQueryGenerator<TEntity>
    where TEntity : IHasKey<TKey>
    where TKey : struct
{

    ValueTask<TEntity> GetAsync(TKey id);

    Task<List<TEntity>> GetAllByIdsAsync(IEnumerable<TKey> ids);

    Task<List<TEntity>> GetAllAsync();
 
    Task<List<TEntity>> Filter(JsonConditionBase? jsonCondition, List<SortModel>? sort, PagingModel? paging);
}
