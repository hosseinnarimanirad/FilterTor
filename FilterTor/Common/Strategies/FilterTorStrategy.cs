using FilterTor.Conditions;
using FilterTor.Decorators;
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
        _sortResolver = sortResolver;
        _entityResolver = entityResolver;
    }

    public abstract IQueryable<TEntity> Filter(IQueryable<TEntity> queryable, JsonConditionBase? jsonCondition, List<SortModel>? sorts, PagingModel? paging);

}
