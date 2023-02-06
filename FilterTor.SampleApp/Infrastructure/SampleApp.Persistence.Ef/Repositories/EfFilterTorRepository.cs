namespace SampleApp.Persistence.Ef.Repositories;

using FilterTor.Conditions;
using FilterTor.Decorators;
using FilterTor.Models;
using FilterTor.Strategies;
using Grid.Persistence;
using SampleApp.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public abstract class EfFilterTorRepository<TKey, TEntity> : EfQueryRepository<TKey, TEntity>
                                                                where TEntity : class, IHasKey<TKey>
                                                                where TKey : struct
{
    protected FilterTorStrategy<TEntity> Strategy { get; private set; }

    protected EfFilterTorRepository(SampleAppContext dbContext, FilterTorStrategy<TEntity> strategy) : base(dbContext)
    {
        Strategy = strategy;
    }


    public virtual async Task<List<TEntity>> Filter(JsonConditionBase? jsonCondition, List<SortModel>? sorts, PagingModel? paging)
    {
        return await Strategy.Filter(this._context.Set<TEntity>().AsQueryable(), jsonCondition, sorts, paging);

        //IQueryGenerator<TEntity> result = this;

        //if (jsonCondition is not null)
        //{
        //    result = new FilterDecorator<TEntity>(result, jsonCondition, _entityResolver);
        //}

        //if (!sorts.IsNullOrEmpty())
        //{
        //    result = new SortDecorator<TEntity>(result, sorts!, _sortResolver);
        //}

        //if (paging is not null)
        //{
        //    result = new PagingDecorator<TEntity>(result, paging);
        //}

        //return await result.Query(this._context.Set<TEntity>().AsQueryable()).ToListAsync();
    }
}