using FilterTor.Conditions;
using FilterTor.Models;
using FilterTor.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterTor.Strategies;

// Context class in strategy design pattern
public class FilterTorStrategyContext<TEntity>
{
    private FilterTorStrategy<TEntity> _strategy;

    public FilterTorStrategyContext(SingleSourceStrategy<TEntity> strategy)
    {
        this._strategy = strategy;
    }

    public void SetStrategy(FilterTorStrategy<TEntity> strategy)
    {
        this._strategy = strategy;
    }

    public IQueryable<TEntity> Filter(IQueryable<TEntity> queryable, JsonConditionBase? jsonCondition, List<SortModel>? sorts, PagingModel? paging)
    {
        return _strategy.Filter(queryable, jsonCondition, sorts, paging);
    }
}
