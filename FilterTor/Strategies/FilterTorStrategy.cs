using FilterTor.Conditions;
using FilterTor.Models;
using FilterTor.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterTor.Strategies;

public abstract class FilterTorStrategy<TEntity>
{
    protected ISortResolver<TEntity> _sortResolver;

    protected IEntityResolver<TEntity> _entityResolver;

    protected FilterTorStrategy(ISortResolver<TEntity> sortResolver, IEntityResolver<TEntity> entityResolver)
    {
        this._sortResolver = sortResolver;
        this._entityResolver = entityResolver;
    }

    public abstract Task<List<TEntity>> Filter(JsonConditionBase? jsonCondition, List<SortModel>? sorts, PagingModel? paging);
}
