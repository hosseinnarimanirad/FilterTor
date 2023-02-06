namespace FilterTor.Strategies;

using FilterTor.Conditions;
using FilterTor.Decorators;
using FilterTor.Extensions;
using FilterTor.Models;
using FilterTor.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SingleSourceStrategy<TEntity> : FilterTorStrategy<TEntity>
{
    public SingleSourceStrategy(ISortResolver<TEntity> sortResolver, IEntityResolver<TEntity> entityResolver) : base(sortResolver, entityResolver)
    {

    }

    public override async Task<List<TEntity>> Filter(IQueryable<TEntity> queryable, JsonConditionBase? jsonCondition, List<SortModel>? sorts, PagingModel? paging)
    {
        IQueryGenerator<TEntity> result = new SimpleQueryGenerator<TEntity>(queryable);

        if (jsonCondition is not null)
        {
            result = new FilterDecorator<TEntity>(result, jsonCondition, _entityResolver);
        }

        if (!sorts.IsNullOrEmpty())
        {
            result = new SortDecorator<TEntity>(result, sorts!, _sortResolver);
        }

        if (paging is not null)
        {
            result = new PagingDecorator<TEntity>(result, paging);
        }

        //return await result.Query(this._context.Set<TEntity>().AsQueryable()).ToListAsync();
        return await result.Query(queryable.AsQueryable()).ToListAsync();
    }
}
