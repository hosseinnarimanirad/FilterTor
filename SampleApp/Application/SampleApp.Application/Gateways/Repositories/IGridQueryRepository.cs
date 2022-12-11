namespace SampleApp.Application.Gateways;

using FilterTor.Conditions;
using FilterTor.Decorators;
using FilterTor.Models;
using SampleApp.Core;
using SampleApp.Core.Entities;
using System.Linq.Expressions;


public interface IGridQueryRepository<TKey, TEntity> : IScoped, IQueryGenerator<TEntity>
    where TEntity : IHasKey<TKey>
    where TKey : struct
{
    Task<List<TEntity>> Filter(JsonConditionBase? jsonCondition, List<SortModel>? sort, PagingModel? paging);
}
