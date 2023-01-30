//namespace FilterTor.Common.Decorators;

//using FilterTor.Conditions;
//using FilterTor.Decorators;
//using FilterTor.Extensions;
//using FilterTor.Models;
//using FilterTor.Resolvers;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//public class QueryGenerator<TEntity> : IQueryGenerator<TEntity>

//{
//    protected readonly ISortResolver<TEntity> _sortResolver;
//    protected readonly IEntityResolver<TEntity> _entityResolver;

//    protected QueryGenerator(ISortResolver<TEntity> sortResolver, IEntityResolver<TEntity> entityResolver)
//    {
//        this._sortResolver = sortResolver;
//        this._entityResolver = entityResolver;
//    }

//    public IQueryable<TEntity> Query(IQueryable<TEntity> list)
//    {
//        // simple decorator that does noting just to help
//        // building the chain of decorator classes
//        return list;
//    }

//    public IQueryable<TEntity> Filter(JsonConditionBase? jsonCondition, List<SortModel>? sorts, PagingModel? paging)
//    {
//        IQueryGenerator<TEntity> result = this;

//        if (jsonCondition is not null)
//        {
//            result = new FilterDecorator<TEntity>(result, jsonCondition, _entityResolver);
//        }

//        if (!sorts.IsNullOrEmpty())
//        {
//            result = new SortDecorator<TEntity>(result, sorts!, _sortResolver);
//        }

//        if (paging is not null)
//        {
//            result = new PagingDecorator<TEntity>(result, paging);
//        }

//        return result/*.Query(_entityResolver.GetSource())*/;
//    }
//}