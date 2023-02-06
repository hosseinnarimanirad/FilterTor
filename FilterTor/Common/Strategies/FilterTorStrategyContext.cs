//using FilterTor.Conditions;
//using FilterTor.Models;
//using FilterTor.Resolvers;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FilterTor.Strategies;

//// Context class in strategy design pattern
//public class FilterTorStrategyContext<TEntity>
//{
//    private FilterTorStrategy<TEntity> _strategy;

//    public FilterTorStrategyContext(ISortResolver<TEntity> sortResolver, IEntityResolver<TEntity> entityResolver)
//    {
//        this._strategy = new SingleSourceStrategy<TEntity>(sortResolver, entityResolver);
//    }

//    public void SetStrategy(FilterTorStrategy<TEntity> strategy)
//    {
//        this._strategy = strategy;
//    }

//    public async Task<List<TEntity>> Filter(JsonConditionBase? jsonCondition, List<SortModel>? sorts, PagingModel? paging)
//    {
//        return await _strategy.Filter(jsonCondition, sorts, paging);
//    }
//}
