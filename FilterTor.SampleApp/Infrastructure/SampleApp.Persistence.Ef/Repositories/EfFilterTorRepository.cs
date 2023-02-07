namespace SampleApp.Persistence.Ef.Repositories;

using FilterTor.Conditions;
using FilterTor.Decorators;
using FilterTor.Models;
using FilterTor.Strategies;
using Grid.Persistence;
using Microsoft.EntityFrameworkCore;
using SampleApp.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Context class for strategy design pattern
public abstract class EfFilterTorRepository<TKey, TEntity> : EfQueryRepository<TKey, TEntity>
                                                                where TEntity : class, IHasKey<TKey>
                                                                where TKey : struct
{ 
    protected FilterTorStrategyContext<TEntity> StrategyContext { get; private set; }
    

    protected EfFilterTorRepository(SampleAppContext dbContext, FilterTorStrategyContext<TEntity> strategyContext) : base(dbContext)
    {
        StrategyContext = strategyContext;
    }
     
    public virtual async Task<List<TEntity>> Filter(JsonConditionBase? jsonCondition, List<SortModel>? sorts, PagingModel? paging)
    {
        return await StrategyContext.Filter(this._context.Set<TEntity>().AsQueryable(), jsonCondition, sorts, paging).ToListAsync();

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