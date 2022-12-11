//namespace FilterTor.Common.Decorators;
 
//using FilterTor.Conditions;
//using FilterTor.Decorators;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;
//using FilterTor.Common.Entities;
//using System.ComponentModel.DataAnnotations;

//public class FilterResolver<T> : IFilterResolver<T> where T : class
//{
//    private readonly IEntityResolver<T> _entityResolver;

//    public FilterResolver(IEntityResolver<T> entityResolver)
//    {
//        this._entityResolver = entityResolver;
//    }

//    public Expression<Func<T, bool>>? GetPredicate(JsonConditionBase jsonCondition)
//    {
//        ICondition? condition = ConditionFactory.Create<T>(jsonCondition, _entityResolver);

//        if (condition is null)
//            return t => false;

//        return ConditionFactory.GetExpression<T>(condition);
//    }
//}
